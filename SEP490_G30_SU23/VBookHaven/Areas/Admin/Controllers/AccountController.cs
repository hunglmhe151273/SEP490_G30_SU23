using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.Respository;

namespace VBookHaven.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountRespository _accRespository;

        public AccountController(IAccountRespository accRespository)
        {
            _accRespository = accRespository;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string gmail, string password)
        {
            var acc = await _accRespository.findByEmailAndPassword(gmail, password);
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
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string gmail)
        {
            await _accRespository.ResetPassword(gmail);
            return RedirectToAction("ResetPassword");
        }
    }

}
