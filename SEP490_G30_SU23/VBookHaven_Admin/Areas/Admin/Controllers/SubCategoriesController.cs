using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly VBookHavenDBContext _context;

        public SubCategoriesController(VBookHavenDBContext context)
        {
            _context = context;
        }

        //// GET: Admin/SubCategories
        public async Task<IActionResult> Index()
        {
            var vBookHavenDBContext = _context.SubCategories.Include(s => s.Category);
            return View(await vBookHavenDBContext.ToListAsync());
        }

        //// GET: Admin/SubCategories/Details/5 -- done
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.SubCategories == null)
        //    {
        //        return NotFound();
        //    }

        //    var subCategory = await _context.SubCategories
        //        .Include(s => s.Category)
        //        .FirstOrDefaultAsync(m => m.SubCategoryId == id);
        //    if (subCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(subCategory);
        //}

        // GET: Admin/SubCategories/Create  --done
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubCategoryId,SubCategoryName,CategoryId,Status")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                TempData["success"] = "Tạo loại con thành công";
                return RedirectToAction("Index", "Categories");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: Admin/SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        // POST: Admin/SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubCategoryId,SubCategoryName,CategoryId,Status")] SubCategory subCategory)
        {
            if (id != subCategory.SubCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.SubCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Cập nhật thành công";
                return RedirectToAction("Index", "Categories");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        //// GET: Admin/SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SubCategoryId == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        //// POST: Admin/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubCategories == null)
            {
                return Problem("Entity set 'VBookHavenDBContext.SubCategories'  is null.");
            }
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Categories");
        }

        private bool SubCategoryExists(int id)
        {
          return (_context.SubCategories?.Any(e => e.SubCategoryId == id)).GetValueOrDefault();
        }
    }
}
