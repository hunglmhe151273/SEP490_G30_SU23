using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.Common;

namespace VBookHaven.Controllers
{
    public class AuthorController : Controller
    {
        private VBookHavenDBContext dbContext = new VBookHavenDBContext();
        private CommonGetCode commonGetCode = new CommonGetCode();

        public IActionResult Index()
        {
            var authors = commonGetCode.GetAllAuthors();
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

            Author.Status = true;
            Author.CreateDate = DateTime.Now;
            Author.CreatorId = 1;

            dbContext.Authors.Add(Author);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var author = commonGetCode.GetAuthorById(id);
            if (author == null)
                return NotFound();

            Author = author;
            return View(this);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(int id)
        {
			if (!ModelState.IsValid)
			{
				return View(this);
			}

            Author.EditDate = DateTime.Now;
            Author.EditorId = 1;

            dbContext.Entry<Author>(Author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

			return RedirectToAction("Index");
		}
    }
}
