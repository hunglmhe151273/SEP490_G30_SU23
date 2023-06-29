using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBookHaven.Common;
using VBookHaven.Respository;
using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Areas.Admin.Controllers
{
	public class AddProductViewModel
	{
		public Product Product { get; set; }
		public Book Book { get; set; }
		public Stationery Stationery { get; set; }
		public List<int> AuthorIdList { get; set; }
		public List<IFormFile> AddImageList { get; set; }
		public List<int> DeleteImageIdList { get; set; }

		public AddProductViewModel()
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
		private readonly IProductRespository productRespository;
		private readonly IAuthorRepository authorRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IImageRepository imageRepository;

		public ProductController(IProductRespository productRespository, IAuthorRepository authorRepository, 
			ICategoryRepository categoryRepository, IImageRepository imageRepository)
		{
			this.productRespository = productRespository;
			this.authorRepository = authorRepository;
			this.categoryRepository = categoryRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index()
		{
			var productsTask = productRespository.GetAllProductsAsync();

			var products = await productsTask;
			return View(products);
		}

		public async Task<IActionResult> AddBook()
		{
			var authorsTask = authorRepository.GetAllAuthorsAsync();
			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

			var authors = await authorsTask;
			ViewData["authors"] = new SelectList(authors, "AuthorId", "AuthorName");
			var subCategories = await subCategoriesTask;
			ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

			return View();
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
		public async Task<IActionResult> AddStationery(AddProductViewModel model)
		{
			bool validModel = true;

			ModelState.Remove("Stationery.Product");
			ModelState.Remove("Book.Product");

			if (!ModelState.IsValid)
			{
				validModel = false;
			}
			if (model.Product.Barcode != null)
				if (model.Product.Barcode.StartsWith("PVN"))
					validModel = false;

			if (!validModel)
			{
				var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

				var subCategories = await subCategoriesTask;
				ViewData["subCategories"] = new SelectList(subCategories, "SubCategoryId", "SubCategoryName");

				return View(model);
			}

			model.Product.UnitInStock = 0;
			model.Product.IsBook = false;
			model.Product.Status = true;

			await productRespository.AddStationeryAsync(model.Product, model.Stationery);

			//UploadImages(Product.ProductId);

			return RedirectToAction("Index");
		}

	}
}
