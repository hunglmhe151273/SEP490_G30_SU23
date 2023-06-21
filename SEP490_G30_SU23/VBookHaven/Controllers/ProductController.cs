using Microsoft.AspNetCore.Mvc;
using VBookHaven.Common;
using VBookHaven.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VBookHaven.Controllers
{
	public class ProductController : Controller
	{
		private CommonGetCode commonGetCode = new CommonGetCode();
		private VBookHavenDBContext dbContext = new VBookHavenDBContext();

		public IActionResult Index()
		{
			var products = commonGetCode.GetAllProduct();
			return View(products);
		}

		public IActionResult AddBook()
		{
			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");
			return View();
		}

		[BindProperty]
		public Product Product { get; set; }
		[BindProperty]
		public Book Book { get; set; }

		[HttpPost, ActionName("AddBook")]
		[ValidateAntiForgeryToken]
		public IActionResult AddBookPost()
		{
			ModelState.Remove("Book.Product");
			if (!ModelState.IsValid)
			{
				return View();
			}

			Product.UnitInStock = 0;
			Product.IsBook = true;
			Product.Status = true;
			Product.CreateDate = DateTime.Now;
			Product.CreatorId = 1;

			dbContext.Products.Add(Product);
			dbContext.SaveChanges();

			Book.ProductId = Product.ProductId;
			dbContext.Books.Add(Book);
			dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult EditBook(int id)
		{
			var product = commonGetCode.GetProductById(id);
			var book = commonGetCode.GetBookById(id);

			if (product == null || book == null)
				return NotFound();

			ViewData["product"] = product;
			ViewData["book"] = book;

			var subCategories = commonGetCode.GetAllSubCategories();
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

			return View();
		}

		[HttpPost, ActionName("EditBook")]
		[ValidateAntiForgeryToken]
		public IActionResult EditBookPost(int id)
		{
			if (!ModelState.IsValid)
				return View();

			var oldProduct = dbContext.Products.SingleOrDefault(p => p.ProductId == id);
			var oldBook = dbContext.Books.SingleOrDefault(b => b.ProductId == id);

			if (oldProduct == null || oldBook == null)
				return NotFound();
			
			return RedirectToAction("Index");
		}
	}
}
