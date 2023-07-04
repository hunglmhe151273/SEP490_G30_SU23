using Microsoft.AspNetCore.Mvc;

namespace VBookHaven.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterStaff()
        {
            return View();
        }
        public IActionResult IndexStoreKeepper()
        {
            return View();
        }
    }
}
