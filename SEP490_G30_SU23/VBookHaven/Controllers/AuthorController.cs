using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.Common;

namespace VBookHaven.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            var authors = CommonGetCode.GetAllAuthors();
            return View(authors);
        }

        public IActionResult Add()
        {
            return View();
        }

        [BindProperty]
        public Author Author { get; set; }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbContext = new VBookHavenDBContext();

            Author.Status = true;
            Author.CreateDate = DateTime.Now;
            Author.CreatorId = 1;

            dbContext.Authors.Add(Author);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
