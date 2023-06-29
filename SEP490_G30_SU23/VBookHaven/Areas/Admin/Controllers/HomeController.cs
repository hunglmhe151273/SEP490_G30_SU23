using Microsoft.AspNetCore.Mvc;

namespace VBookHaven.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
