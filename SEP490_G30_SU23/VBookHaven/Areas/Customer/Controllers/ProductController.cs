using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{
	public class ProductBuyingViewModel
	{
		public List<Product> Products { get; set; }
		public List<SubCategory> SubCategories { get; set; }
		public Dictionary<int, string?> Thumbnails { get; set; }

		public ProductBuyingViewModel()
		{
			Products = new List<Product>();
			SubCategories = new List<SubCategory>();
			Thumbnails = new Dictionary<int, string?>();
		}
	}

	public class ProductDetailViewModel
	{
		public Product Product { get; set; }
		public Book Book { get; set; }
		public Stationery Stationery { get; set; }
		public List<Image> Images { get; set; }

		public ProductDetailViewModel()
		{
			Product = new Product();
			Book = new Book();
			Stationery = new Stationery();
			Images = new List<Image>();
		}
	}
	
	[Area("Customer")]
	public class ProductController : Controller
	{
		private readonly IProductRespository productRespository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IAuthorRepository authorRepository;
		private readonly IImageRepository imageRepository;

		public ProductController(IProductRespository productRespository, ICategoryRepository categoryRepository, 
			IAuthorRepository authorRepository, IImageRepository imageRepository)
		{
			this.productRespository = productRespository;
			this.categoryRepository = categoryRepository;
			this.authorRepository = authorRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index()
		{
			var model = new ProductBuyingViewModel();
			
			var productsTask = productRespository.GetAllProductsAsync();
			var subCategoriesTask = categoryRepository.GetAllSubCategoriesAsync();

			model.Products = await productsTask;

			foreach (Product p in model.Products)
			{
				var thumbnail = await imageRepository.GetThumbnailByProductIdAsync(p.ProductId);
				string? name;
				if (thumbnail != null)
					name = thumbnail.ImageName;
				else name = null;
				model.Thumbnails.Add(p.ProductId, name);
			}

			return View(model);
		}

		public async Task<IActionResult> Detail(int id)
		{
			var model = new ProductDetailViewModel();

			var product = await productRespository.GetProductMoreInfoByIdAsync(id);
			if (product == null)
				return NotFound();

			model.Product = product;
			if (product.IsBook == true)
			{
				model.Book = await productRespository.GetBookMoreInfoByIdAsync(id);
			}
			else
			{
				model.Stationery = await productRespository.GetStationeryByIdAsync(id);
			}

			foreach (Image i in model.Product.Images)
			{
				if (i.Status == true)
					model.Images.Add(i);
			}

			return View(model);
		}
	}
}
