using Microsoft.AspNetCore.Mvc;
using VBookHaven.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
		private readonly OrderFunctions functions;

		public HomeController(IProductRespository productRepository, IApplicationUserRespository userRepository,
			ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
		{
			functions = new OrderFunctions(productRepository, userRepository, cartRepository, httpContextAccessor);
		}

		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> IndexFromLogin()
		{
			// Add customer's cart at login
			await functions.AddCartAtLoginAsync();
			return RedirectToAction("Index");
		}
    }
}
