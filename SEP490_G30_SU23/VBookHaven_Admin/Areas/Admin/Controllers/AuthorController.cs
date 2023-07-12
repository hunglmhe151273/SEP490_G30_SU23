using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.DataAccess.Common;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }

        public AuthorViewModel()
        {
            Author = new Author();
        }
    }

    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await authorRepository.GetAllAuthorsAsync();
            return View(authors);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            model.Author.Status = true;

            await authorRepository.AddAuthorAsync(model.Author);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            var model = new AuthorViewModel();
            model.Author = author;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(this);
            }

            await authorRepository.UpdateAuthorAsync(model.Author);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            var author = await authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            author.Status = !author.Status;
            await authorRepository.UpdateAuthorAsync(author);

            return RedirectToAction("Index");
        }
    }
}
