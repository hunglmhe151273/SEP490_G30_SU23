using Microsoft.AspNetCore.Mvc;
using VBookHaven.Common;
using VBookHaven.Models;

namespace VBookHaven.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddBook()
		{
			ViewData["SubCategories"] = CommonGetCode.GetAllSubCategories();
			ViewData["Authors"] = CommonGetCode.GetAllAuthors();
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
			return RedirectToAction("Index");
		}
	}
}
