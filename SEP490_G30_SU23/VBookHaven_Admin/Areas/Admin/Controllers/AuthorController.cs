using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.DataAccess.Common;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using Microsoft.EntityFrameworkCore;
using VBookHaven.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using VBookHaven.Utility;

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
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public AuthorController()
        {
        }

        public async Task<IActionResult> Index()
        {
            var authors = await authorRepository.GetAllAuthorsAsync();
            return View(authors);
        }

        //public IActionResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(AuthorViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    model.Author.Status = true;

        //    await authorRepository.AddAuthorAsync(model.Author);

        //    return RedirectToAction("Index");
        //}

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

        //public async Task<IActionResult> ChangeStatus(int id)
        //{
        //    var author = await authorRepository.GetAuthorByIdAsync(id);
        //    if (author == null)
        //        return NotFound();

        //    author.Status = !author.Status;
        //    await authorRepository.UpdateAuthorAsync(author);

        //    return RedirectToAction("Index");
        //}

        /*---------- Other codes ----------*/

        #region Call API

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Author author)
        {
			if (!ModelState.IsValid)
			{
				return StatusCode(404, "Dữ liệu khi thêm tác giả không đúng tiêu chuẩn!");
            }
            try
			{
				await authorRepository.AddAuthorAsync(author);
                return Ok(author);
			}
			catch (Exception ex)
			{
				return StatusCode(404, "Có lỗi khi thêm tác giả mới!");
			}
		}

        [HttpPost]
		public async Task<IActionResult> ChangeStatus([FromBody] int id)
		{
            try
            {
				var author = await authorRepository.GetAuthorByIdAsync(id);
				if (author == null)
					return StatusCode(404, "Tác giả không tồn tại!");

				author.Status = !author.Status;
				await authorRepository.UpdateAuthorAsync(author);

				return Ok();
			}
            catch (Exception ex)
            {
                return StatusCode(400, "Có lỗi khi đổi trạng thái tác giả!");
            }
		}

		#endregion
	}
}
