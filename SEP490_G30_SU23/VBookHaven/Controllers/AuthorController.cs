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

            ViewData["author"] = author;
            return View();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(int id)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

            var oldAuthor = dbContext.Authors.SingleOrDefault(a => a.AuthorId == id);
            if (oldAuthor == null)
                return NotFound();

            oldAuthor.AuthorName = Author.AuthorName;
            oldAuthor.Description = Author.Description;
            oldAuthor.Status = Author.Status;

            oldAuthor.EditDate = DateTime.Now;
            oldAuthor.EditorId = 1;

            dbContext.SaveChanges();

			return RedirectToAction("Index");
		}
    }
}
