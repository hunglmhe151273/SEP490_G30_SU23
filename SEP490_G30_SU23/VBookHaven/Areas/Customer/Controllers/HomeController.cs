using Microsoft.AspNetCore.Mvc;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;

namespace VBookHaven.Areas.Customer.Controllers
{
	public class HomeViewModel
	{
		public List<Product> FamousProducts { get; set; }
		public List<Product> DiscountProducts { get; set; }
		public List<Product> NewProducts { get; set; }
		public Dictionary<int, string?> ProductsThumbnails { get; set; }

		public HomeViewModel()
		{
			FamousProducts = new List<Product>();
			DiscountProducts = new List<Product>();
			NewProducts = new List<Product>();

			ProductsThumbnails = new Dictionary<int, string?>();
		}
	}
	
	[Area("Customer")]
    public class HomeController : Controller
    {
		private readonly OrderFunctions functions;
		private readonly IProductRespository productRepository;
		private readonly IImageRepository imageRepository;

		public HomeController(IProductRespository productRepository, IApplicationUserRespository userRepository,
			ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor, 
			IImageRepository imageRepository)
		{
			functions = new OrderFunctions(productRepository, userRepository, cartRepository, httpContextAccessor, imageRepository);
			this.productRepository = productRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index()
        {
			//return RedirectToAction("Index", "Product");

			var model = new HomeViewModel();
			var allProducts = await productRepository.GetAllProductsWithImageAsync();
			allProducts = allProducts.Where(p => p.Status == true && p.AvailableUnit > 0).ToList();

			model.FamousProducts = allProducts.ToList();
			model.FamousProducts.Sort((a, b) => productRepository.GetTimesBoughtProductById(a.ProductId).CompareTo(productRepository.GetTimesBoughtProductById(b.ProductId)));
			model.FamousProducts = model.FamousProducts.GetRange(0, Math.Min(model.FamousProducts.Count, 4));

			model.DiscountProducts = allProducts.Where(p => p.RetailDiscount > 0)
				.OrderByDescending(p => p.RetailDiscount).ToList();
			model.DiscountProducts = model.DiscountProducts.GetRange(0, Math.Min(model.DiscountProducts.Count, 4));

			model.NewProducts = allProducts.OrderByDescending(p => p.ProductId).ToList();
			model.NewProducts = model.NewProducts.GetRange(0, Math.Min(model.NewProducts.Count, 8));

			foreach (var product in allProducts)
			{
				int id = product.ProductId;
				Image? thumbnail = await imageRepository.GetThumbnailByProductIdAsync(id);

				string? thumbnailName;
				if (thumbnail == null)
				{
					thumbnailName = null;
				}
				else thumbnailName = thumbnail.ImageName;
				model.ProductsThumbnails.Add(id, thumbnailName);
			}	

			if (TempData["orderSuccess"] != null)
			{
				TempData["success"] = "Đặt hàng thành công";
			}	

			return View(model);
        }

		public async Task<IActionResult> IndexFromLogin()
		{
			// Add customer's cart at login
			await functions.AddCartAtLoginAsync();
			return RedirectToAction("Index");
		}
    }
}
