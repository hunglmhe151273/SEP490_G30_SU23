using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBookHaven.DataAccess.Common;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
	// Khi ModelState co van de luc add, edit -> khong luu lai truong input da nhap, sua
	//	-> Possible solution: form submission with jquery AJAX - no need to reload page?
	//	-> HOAC dat san value cho input; nho chuyen gia tri null thanh empty

	// Kiem tra xem paging front end hoat dong ko
	// Khi de trong ten san pham -> Chua co warning
	// Sort o index - hinh nhu chi co tac dung sort trang dang hien (ko phai sort tat ca list product)

	// Add author, category... la 1 pop up o add product
	// Cho subcategories vao select list group
	// Add product con mat thoi gian, trang khong show la dang load
	// Khi change status product - khong luu lai thong tin thay doi o tren
	// Add anh - dung cach khac de tao random name?

	// Authorize - lay user info tu session(?)
	// Khi author bi disabled thi tinh sao?

	public class ProductManagementViewModel
	{
		public Product Product { get; set; }
		public Book Book { get; set; }
		public Stationery Stationery { get; set; }
		public List<int> AuthorIdList { get; set; }
		public List<IFormFile> AddImageList { get; set; }
		public List<int> DeleteImageIdList { get; set; }

		public ProductManagementViewModel()
		{
			Product = new Product();
			Book = new Book();
			Stationery = new Stationery();
			AuthorIdList = new List<int>();
			AddImageList = new List<IFormFile>();
			DeleteImageIdList = new List<int>();
		}
	}
	
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductRespository _productRespository;
		private readonly IAuthorRepository authorRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IImageRepository imageRepository;

		public ProductController(IProductRespository productRespository, IAuthorRepository authorRepository, 
			ICategoryRepository categoryRepository, IImageRepository imageRepository)
		{
			_productRespository = productRespository;
			this.authorRepository = authorRepository;
			this.categoryRepository = categoryRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index()
		{
			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

            List<Product> products = await _productRespository.GetAllProductsAsync();

			var thumbnails = new Dictionary<int, string?>();
			foreach (Product p in products)
			{
				var thumbnail = await imageRepository.GetThumbnailByProductIdAsync(p.ProductId);
				string? name;
				if (thumbnail != null)
					name = thumbnail.ImageName;
				else name = null;
				thumbnails.Add(p.ProductId, name);
			}
			ViewData["thumbnails"] = thumbnails;

			var subCategories = await subCategoriesTask;
			subCategories.Insert(0, new SubCategory
			{
				SubCategoryId = 0,
				SubCategoryName = "--Không--"
			});
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

			var statusList = new[]
			{
				new { Value = -1, Name = "--Không--"},
				new { Value = 1, Name = "Hoạt động" },
				new { Value = 0, Name = "Đình chỉ" }
			}.ToList();
			ViewData["statusList"] = new SelectList(statusList, "Value", "Name");

			return View(products);
		}

		public async Task<IActionResult> AddBook()
		{
			var authorsTask = authorRepository.GetAllAuthorsAsync();
			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

			var authors = await authorsTask;
			ViewData["authors"] = new MultiSelectList(authors, "AuthorId", "AuthorName");
			var subCategories = await subCategoriesTask;
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddBook(ProductManagementViewModel model)
		{
			var validateBarcodeTask = ValidateBarcodeAsync(model.Product.Barcode);
			
			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			ModelState.Remove("Book.Product");

			if (!ModelState.IsValid)
			{
				validModel = false;
			}

			var validatebarCode = await validateBarcodeTask;
			if (!validatebarCode.Equals(""))
				validModel = false;
			
			if (!validModel)
			{
				var authorsTask = authorRepository.GetAllAuthorsAsync();
				var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

				var authors = await authorsTask;
				ViewData["authors"] = new MultiSelectList(authors, "AuthorId", "AuthorName");
				var subCategories = await subCategoriesTask;
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

				return View(model);
			}

			model.Product.UnitInStock = 0;
			model.Product.IsBook = true;
			model.Product.Status = true;

			await _productRespository.AddBookAsync(model.Product, model.Book);

			var addAuthorTask = _productRespository.AddAuthorsToBookAsync(model.Product.ProductId, model.AuthorIdList);
			var uploadImageTask = imageRepository.UploadImagesAsync(model.Product.ProductId, model.AddImageList);

			Task.WaitAll(addAuthorTask, uploadImageTask);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> AddStationery()
		{
			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

			var subCategories = await subCategoriesTask;
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddStationery(ProductManagementViewModel model)
		{
			var validateBarcodeTask = ValidateBarcodeAsync(model.Product.Barcode);

			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			ModelState.Remove("Book.Product");

			if (!ModelState.IsValid)
			{
				validModel = false;
			}

			var validatebarCode = await validateBarcodeTask;
			if (!validatebarCode.Equals(""))
				validModel = false;

			if (!validModel)
			{
				var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

				var subCategories = await subCategoriesTask;
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", model.Product.SubCategoryId);

				return View(model);
			}

			model.Product.UnitInStock = 0;
			model.Product.IsBook = false;
			model.Product.Status = true;

			await _productRespository.AddStationeryAsync(model.Product, model.Stationery);

			await imageRepository.UploadImagesAsync(model.Product.ProductId, model.AddImageList);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> EditBook(int id)
		{
			var product = await _productRespository.GetProductByIdAsync(id);
			var book = await _productRespository.GetBookByIdAsync(id);

			if (product == null || book == null)
				return NotFound();

			var model = new ProductManagementViewModel();
			model.Product = product;
			model.Book = book;

			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();
			var authorsTask = authorRepository.GetAllAuthorsAsync();
			var bookAuthorsTask = authorRepository.GetAuthorsByBookIdAsync(id);
			var imageListTask = imageRepository.GetImagesByProductIdAsync(id);

			var subCategories = await subCategoriesTask;
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

			var bookAuthors = await bookAuthorsTask;
			var bookAuthorsId = new List<int>();
			foreach (Author a in bookAuthors)
				bookAuthorsId.Add(a.AuthorId);
			var authors = await authorsTask;
			ViewData["bookAuthorsId"] = bookAuthorsId;
			ViewData["authors"] = authors;

			// Khong lay duoc selected items tu Multi Select List
			//ViewData["authors"] = new MultiSelectList(authors, "AuthorId", "AuthorName", bookAuthorsId);
			
			var imageList = await imageListTask;
			ViewData["images"] = imageList;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditBook(int id, ProductManagementViewModel model)
		{
			var validateBarcodeTask = ValidateBarcodeAsync(model.Product.Barcode, id);

			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			ModelState.Remove("Book.Product");

			if (!ModelState.IsValid)
			{
				validModel = false;
			}

			var validatebarCode = await validateBarcodeTask;
			if (!validatebarCode.Equals(""))
				validModel = false;

			if (!validModel)
			{
				var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();
				var authorsTask = authorRepository.GetAllAuthorsAsync();
				var imageListTask = imageRepository.GetImagesByProductIdAsync(id);
				
				var subCategories = await subCategoriesTask;
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", model.Product.SubCategoryId);

				var authors = await authorsTask;
				ViewData["authors"] = authors;
				ViewData["bookAuthorsId"] = model.AuthorIdList;

				var imageList = await imageListTask;
				ViewData["images"] = imageList;

				return View(model);
			}

			if (model.Product.Barcode == null)
				model.Product.Barcode = "PVN" + id;

			var deleteImageTask = imageRepository.DeleteImageListAsync(model.DeleteImageIdList);
			var uploadImageTask = imageRepository.UploadImagesAsync(id, model.AddImageList);

			var updateProductTask = _productRespository.UpdateProductAsync(model.Product);

			await _productRespository.UpdateBookAsync(model.Book);
			await _productRespository.UpdateBookAuthorsAsync(id, model.AuthorIdList);

			Task.WaitAll(deleteImageTask, uploadImageTask, updateProductTask);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> EditStationery(int id)
		{
			var product = await _productRespository.GetProductByIdAsync(id);
			var stationery = await _productRespository.GetStationeryByIdAsync(id);

			if (product == null || stationery == null)
				return NotFound();

			var model = new ProductManagementViewModel();
			model.Product = product;
			model.Stationery = stationery;

			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();
			var imageListTask = imageRepository.GetImagesByProductIdAsync(id);

			var subCategories = await subCategoriesTask;
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

			var imageList = await imageListTask;
			ViewData["images"] = imageList;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditStationery(int id, ProductManagementViewModel model)
		{
			var validateBarcodeTask = ValidateBarcodeAsync(model.Product.Barcode, id);

			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			ModelState.Remove("Book.Product");

			if (!ModelState.IsValid)
			{
				validModel = false;
			}

			var validatebarCode = await validateBarcodeTask;
			if (!validatebarCode.Equals(""))
				validModel = false;

			if (!validModel)
			{
				var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();
				var imageListTask = imageRepository.GetImagesByProductIdAsync(id);

				var subCategories = await subCategoriesTask;
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", model.Product.SubCategoryId);

				var imageList = await imageListTask;
				ViewData["images"] = imageList;
				
				return View(model);
			}

			if (model.Product.Barcode == null)
				model.Product.Barcode = "PVN" + id;

			var deleteImageTask = imageRepository.DeleteImageListAsync(model.DeleteImageIdList);
			var uploadImageTask = imageRepository.UploadImagesAsync(id, model.AddImageList);

			var updateProductTask = _productRespository.UpdateProductAsync(model.Product);
			var updateStationeryTask = _productRespository.UpdateStationeryAsync(model.Stationery);

			Task.WaitAll(deleteImageTask, uploadImageTask, updateProductTask, updateStationeryTask);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ViewBook(int id)
		{
			var productTask = _productRespository.GetProductMoreInfoByIdAsync(id);
			var bookTask = _productRespository.GetBookMoreInfoByIdAsync(id);

			var product = await productTask;
			var book = await bookTask;

			if (product == null || book == null)
				return NotFound();

			var model = new ProductManagementViewModel()
			{
				Product = product,
				Book = book
			};

			return View(model);
		}

		public async Task<IActionResult> ViewStationery(int id)
		{
			var productTask = _productRespository.GetProductMoreInfoByIdAsync(id);
			var stationeryTask = _productRespository.GetStationeryByIdAsync(id);

			var product = await productTask;
			var stationery = await stationeryTask;

			if (product == null || stationery == null)
				return NotFound();

			var model = new ProductManagementViewModel()
			{
				Product = product,
				Stationery = stationery
			};

			return View(model);
		}

		public async Task<IActionResult> ChangeStatusProduct(int id)
		{
			var success = await _productRespository.ChangeStatusProductAsync(id);
			if (!success)
				return NotFound();
			else
				return RedirectToAction("Index");
		}

		//---------- Other functions ----------

		async Task<string> ValidateBarcodeAsync(string? barcode)
		{
			if (barcode == null)
				return "";
			
			if (barcode.StartsWith("PVN"))
					return "Mã vạch không được có tiền tố PVN của hệ thống";

			var product = await _productRespository.GetProductByBarcodeAsync(barcode);
			if (product != null)
				return "Mã vạch đã tồn tại";

			return "";
		}

		async Task<string> ValidateBarcodeAsync(string? barcode, int id)
		{
			if (barcode == null)
				return "";

			if (barcode.StartsWith("PVN") && !barcode.Equals("PVN" + id))
				return "Mã vạch không được có tiền tố PVN của hệ thống";

			var product = await _productRespository.GetProductByBarcodeAsync(barcode);
			if (product != null)
			{
				if (product.ProductId != id)
					return "Mã vạch đã tồn tại";
			}
				
			return "";
		}

	}
}
