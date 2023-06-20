using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.Respository;

namespace VBookHaven.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IAccountRespository _accRespository;

        public LoginController(IAccountRespository accRespository)
        {
            _accRespository = accRespository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var acc = await _accRespository.findByEmailAndPassword(email, password);
            if (acc is not null)
            {
                HttpContext.Session.SetString("account", JsonSerializer.Serialize(acc));

                //read session
                var session = HttpContext.Session;
                string key_access = "account";
                string json = session.GetString(key_access);
                Console.WriteLine(json);

                //TODO: return Page
                //if (acc.RoleId == 3)
                //{
                //    return RedirectToPage("/admin/category/index");
                //}
                //if (acc.RoleId == 2)
                //{
                //    return RedirectToPage("/employee/product/viewproduct");
                //}
                //else
                //{
                //    return RedirectToPage("/index");
                //}
            }
            else
            {
                ViewData["message"] = "This account doesn't exist";
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
    }

}
