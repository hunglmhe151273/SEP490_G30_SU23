using Microsoft.AspNetCore.Mvc;
using VBookHaven.Common;
using VBookHaven.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace VBookHaven.Controllers
{
	// Chua co kiem tra unique Barcode
	// Khi Model State co van de, thay doi voi Authors khong duoc luu lai -> Sua thanh multiple select list?
	// Khi Model State co van de, delete image ko dc luu lai (co the sua), added image ko dc luu lai (kinda impossible)

	// Sua delete thumbnail thanh button -> hide thumbnail di
	// Paging, filter, search trong product management
	// Sua design theo mock up?
	
	// Don code - Cho function, design lap vao 1 file rieng - Cho vao repository, js file, html layout...
	// Authorize - lay user info tu session(?)
	
	// Khi author bi disabled thi tinh sao?
	// Khong hien khi product bi disabled

	public class ProductController : Controller
	{
		private CommonGetCode commonGetCode;
		private VBookHavenDBContext dbContext;
		private readonly IWebHostEnvironment webHostEnvironment;
		public ProductController(IWebHostEnvironment webHostEnvironment)
		{
			commonGetCode = new CommonGetCode();
			dbContext = new VBookHavenDBContext();
			this.webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			var products = commonGetCode.GetAllProduct();
			return View(products);
		}

		public IActionResult AddBook()
		{
			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");
			var authors = commonGetCode.GetAllAuthors();
			ViewData["authors"] = new SelectList(authors, "AuthorId", "AuthorName");
			return View();
		}

		[BindProperty]
		public Product Product { get; set; }
		[BindProperty]
		public Book Book { get; set; }
		[BindProperty]
		public List<int> AuthorIdList { get; set; }
		[BindProperty]
		public IFormFile? Thumbnail { get; set; }
		[BindProperty]
		public bool DoDeleteThumbnail { get; set; }
		[BindProperty]
		public List<IFormFile> AddImageList { get; set; }
		[BindProperty]
		public List<int> DeleteImageIdList { get; set; }

		private void UploadThumbnail(int id)
		{
			string wwwRootPath = webHostEnvironment.WebRootPath;
			if (Thumbnail != null)
			{
				string fileName = "thumbnail_" + id + Path.GetExtension(Thumbnail.FileName);
				string thumbnailPath = Path.Combine(wwwRootPath, @"images\thumbnail");

				using (var fileStream = new FileStream(Path.Combine(thumbnailPath, fileName), FileMode.Create))
				{
					Thumbnail.CopyTo(fileStream);
				}

				Product.Thumbnail = fileName;
			}
		}

		private void ChangeThumbnail(int id)
		{
			string wwwRootPath = webHostEnvironment.WebRootPath;
			string? fileName = commonGetCode.GetProductById(id).Thumbnail;
			string path = "";
			if (fileName != null)
			{
				path = Path.Combine(wwwRootPath, @"images\thumbnail", fileName);
			}

			if (Thumbnail != null)
			{
				if (!path.IsNullOrEmpty())
				{
					FileInfo file = new FileInfo(path);
					if (file.Exists)
						file.Delete();
				}
				UploadThumbnail(id);
			}
			else if (DoDeleteThumbnail)
			{
				if (!path.IsNullOrEmpty())
				{
					FileInfo file = new FileInfo(path);
					if (file.Exists)
						file.Delete();
				}
				Product.Thumbnail = null;
			}
		}

		public void UploadImages(int id)
		{
			string wwwRootPath = webHostEnvironment.WebRootPath;
			foreach (IFormFile image in AddImageList)
			{
				if (image != null)
				{
					Image imageInfo = new Image();
					imageInfo.CreateDate = DateTime.Now;
					imageInfo.CreatorId = 1;
					imageInfo.Status = true;
					imageInfo.ProductId = id;

					dbContext.Images.Add(imageInfo);
					dbContext.SaveChanges();

					string fileName = "image_" + imageInfo.ImageId + Path.GetExtension(image.FileName);
					string imagePath = Path.Combine(wwwRootPath, @"images\img");

					using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
					{
						image.CopyTo(fileStream);
					}

					imageInfo.ImageName = fileName;
					dbContext.SaveChanges();
				}
			}
			
		}

		public void DeleteImage()
		{
			foreach (int i in DeleteImageIdList)
			{
				var image = commonGetCode.GetImageById(i);
				if (image != null)
				{
					image.EditDate = DateTime.Now;
					image.EditorId = 1;
					image.Status = false;
					dbContext.Entry<Image>(image).State = EntityState.Modified;
				}
			}

			dbContext.SaveChanges();
		}

		[HttpPost, ActionName("AddBook")]
		[ValidateAntiForgeryToken]
		public ActionResult AddBookPost()
		{
			bool validModel = true;

			ModelState.Remove("Book.Product");
			if (!ModelState.IsValid)
			{
				validModel = false;
			}
			if (Product.Barcode != null)
				if (Product.Barcode.StartsWith("PVN"))
					validModel = false;
			
			if (!validModel)
			{
				var subCategories = commonGetCode.GetAllSubCategories();
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");
				var authors = commonGetCode.GetAllAuthors();
				ViewData["authors"] = new SelectList(authors, "AuthorId", "AuthorName");
				return View();
			}

			Product.UnitInStock = 0;
			Product.IsBook = true;
			Product.Status = true;
			Product.CreateDate = DateTime.Now;
			Product.CreatorId = 1;

			dbContext.Products.Add(Product);
			dbContext.SaveChanges();

			if (Product.Barcode == null)
				Product.Barcode = "PVN" + Product.ProductId;

			Book.ProductId = Product.ProductId;

			dbContext.Books.Add(Book);

			UploadThumbnail(Product.ProductId);
			UploadImages(Product.ProductId);
			dbContext.SaveChanges();

			foreach (int id in AuthorIdList)
			{
				var author = commonGetCode.GetAuthorById(id);
				if (author != null)
					Book.Authors.Add(author);
			}
			dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult EditBook(int id)
		{
			var product = commonGetCode.GetProductById(id);
			var book = commonGetCode.GetBookById(id);

			if (product == null || book == null)
				return NotFound();

			Product = product;
			Book = book;

			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

			var authors = commonGetCode.GetAuthorsByBookId(id);
			var otherAuthors = commonGetCode.GetAllAuthors();
			foreach (Author a in authors)
				otherAuthors.Remove(a);
			ViewData["authors"] = authors;
			ViewData["otherAuthors"] = new SelectList(otherAuthors, "AuthorId", "AuthorName");

			var imageList = commonGetCode.GetImagesByProductId(id);
			ViewData["images"] = imageList;

			return View(this);
		}

		[HttpPost, ActionName("EditBook")]
		[ValidateAntiForgeryToken]
		public IActionResult EditBookPost(int id)
		{
			bool validModel = true;
			
			ModelState.Remove("Book.Product");
			if (!ModelState.IsValid)
			{
				validModel = false;
			}
			if (Product.Barcode != null)
				if (Product.Barcode.StartsWith("PVN") && !Product.Barcode.Equals("PVN" + Product.ProductId))
					validModel = false;

			if (!validModel)
			{
				var subCategories = commonGetCode.GetAllSubCategories();
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", Product.SubCategoryId);
				
				var authors = commonGetCode.GetAuthorsByBookId(id);
				var otherAuthors = commonGetCode.GetAllAuthors();
				foreach (Author a in authors)
					otherAuthors.Remove(a);
				ViewData["authors"] = authors;
				ViewData["otherAuthors"] = new SelectList(otherAuthors, "AuthorId", "AuthorName");

				var imageList = commonGetCode.GetImagesByProductId(id);
				ViewData["images"] = imageList;

				return View(this);
			}

			Product.EditDate = DateTime.Now;
			Product.EditorId = 1;

			if (Product.Barcode == null)
				Product.Barcode = "PVN" + Product.ProductId;

			ChangeThumbnail(id);
			DeleteImage();
			UploadImages(id);

			dbContext.Entry<Product>(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			dbContext.Entry<Book>(Book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			dbContext.SaveChanges();

			var book = dbContext.Books.Include(b => b.Authors).SingleOrDefault(b => b.ProductId == id);
			book.Authors.Clear();
			var allAuthors = dbContext.Authors.ToList();
			foreach (int i in AuthorIdList)
			{
				book.Authors.Add(allAuthors.SingleOrDefault(a => a.AuthorId == i));
			}
			dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult AddStationery()
		{
			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");
			return View();

			return View();
		}

		[BindProperty]
		public Stationery Stationery { get; set; }

		[HttpPost, ActionName("AddStationery")]
		[ValidateAntiForgeryToken]
		public IActionResult AddStationeryPost()
		{
			bool validModel = true;
			
			ModelState.Remove("Stationery.Product");
			if (!ModelState.IsValid)
			{
				validModel = false;
			}
			if (Product.Barcode != null)
				if (Product.Barcode.StartsWith("PVN"))
					validModel = false;

			if (!validModel)
			{
				var subCategories = commonGetCode.GetAllSubCategories();
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");
				return View();
			}

			Product.UnitInStock = 0;
			Product.IsBook = false;
			Product.Status = true;
			Product.CreateDate = DateTime.Now;
			Product.CreatorId = 1;

			dbContext.Products.Add(Product);
			dbContext.SaveChanges();

			if (Product.Barcode == null)
				Product.Barcode = "PVN" + Product.ProductId;

			Stationery.ProductId = Product.ProductId;
			dbContext.Stationeries.Add(Stationery);

			UploadThumbnail(Product.ProductId);
			UploadImages(Product.ProductId);
			dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult EditStationery(int id)
		{
			var product = commonGetCode.GetProductById(id);
			var stationery = commonGetCode.GetStationeryById(id);

			if (product == null || stationery == null)
				return NotFound();

			Product = product;
			Stationery = stationery;

			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

			var imageList = commonGetCode.GetImagesByProductId(id);
			ViewData["images"] = imageList;

			return View(this);
		}

		[HttpPost, ActionName("EditStationery")]
		[ValidateAntiForgeryToken]
		public IActionResult EditStationeryPost(int id)
		{
			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			if (!ModelState.IsValid)
			{
				validModel = false;
			}
			if (Product.Barcode != null)
				if (Product.Barcode.StartsWith("PVN") && !Product.Barcode.Equals("PVN" + Product.ProductId))
					validModel = false;

			if (!validModel)
			{
				var subCategories = commonGetCode.GetAllSubCategories();
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", Product.SubCategoryId);
				
				var imageList = commonGetCode.GetImagesByProductId(id);
				ViewData["images"] = imageList;
				return View(this);
			}

			Product.EditDate = DateTime.Now;
			Product.EditorId = 1;

			if (Product.Barcode == null)
				Product.Barcode = "PVN" + Product.ProductId;

			ChangeThumbnail(id);
			DeleteImage();
			UploadImages(id);

			dbContext.Entry<Product>(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			dbContext.Entry<Stationery>(Stationery).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			dbContext.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
