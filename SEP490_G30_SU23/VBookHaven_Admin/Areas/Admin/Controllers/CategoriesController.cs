using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.Utility;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
    public class CategoriesController : Controller
    {
        private readonly VBookHavenDBContext _context;
        [ActivatorUtilitiesConstructor]
        public CategoriesController(VBookHavenDBContext context)
        {
            _context = context;
        }

        public CategoriesController()
        {
        }

        // GET: Admin/Categories -- done
        public async Task<IActionResult> Index() 
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.Include(c => c.SubCategories).ToListAsync()) :
                          Problem("Danh sách nhóm sản phẩm rỗng");
        }

        //// GET: Admin/Categories/Details/5  -- Done
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Categories == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Categories.Include(c => c.SubCategories)
        //        .FirstOrDefaultAsync(m => m.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: Admin/Categories/Create -done
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create -- done
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Tạo loại sản phẩm thành công";
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5  -- done
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5   --done
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //// GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //// POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'VBookHavenDBContext.Categories'  is null.");
            }
            var category = await _context.Categories.Include(cate => cate.SubCategories).ThenInclude(sub => sub.Products).SingleOrDefaultAsync( c => c.CategoryId == id);
            if (category != null)
            {
                if (category.SubCategories.SelectMany(sub => sub.Products).Count() > 0)
                {
                    TempData["error"] = "Không thể xóa dữ liệu đang được sử dụng";
                    return RedirectToAction("Index", "Categories");
                }
                _context.Categories.Remove(category);
            }
            TempData["success"] = "Xóa thành công";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
