using Microsoft.AspNetCore.Mvc;

namespace VBookHaven.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
