using Microsoft.AspNetCore.Mvc;

namespace VBookHaven.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class OrderController : Controller
	{
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddToCart(int number, int id)
		{	
			// TODO: Use session?
			
			return RedirectToAction("Detail", "Product", new { id = id });
		}
	}
}
