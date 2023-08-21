using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Models.ViewModels;
using VBookHaven.Utility;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
    public class SuppliersController : Controller
    {
        private readonly VBookHavenDBContext _context;

        IMapper _mapper;
        [ActivatorUtilitiesConstructor]
        public SuppliersController(VBookHavenDBContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public SuppliersController()
        {
        }

        // GET: Admin/Suppliers
        public async Task<IActionResult> Index()
        {
            return _context.Suppliers != null ?
                        View(await _context.Suppliers.ToListAsync()) :
                        Problem("Entity set 'VBookHavenDBContext.Suppliers'  is null.");
        }

        // GET: Admin/Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return RedirectToAction(nameof(Index));
            }

        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(m => m.SupplierId == id);
        var productIds = _context.PurchaseOrders
                        .Where(order => order.SupplierId == id)
                        .SelectMany(order => order.PurchaseOrderDetails)
                        .Select(detail => detail.ProductId)
                        .Distinct()
                        .ToList();
        var products = _context.Products
            .Where(product => productIds.Contains(product.ProductId))
            .Include(product => product.Images)
            .Include(product => product.SubCategory)
            .ToList();
            SupplierDetailsVM model = new SupplierDetailsVM();
            model.Supplier = supplier;
            model.SupplierProducts = products;
            if (supplier == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Admin/Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,Address,SupplierName,Phone,Status,Description,Email,ProvinceCode,Province,DistrictCode,District,WardCode,Ward")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Admin/Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,Address,SupplierName,Phone,Status,Description,Email,ProvinceCode,Province,DistrictCode,District,WardCode,Ward")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Admin/Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'VBookHavenDBContext.Suppliers'  is null.");
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }

        #region CallAPI
        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDTO suppilerDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404, "Lỗi xảy ra! Vui lòng thử lại sau.");
            }
            try
            {
                Supplier? supplier = _mapper.Map<SupplierDTO, Supplier>(suppilerDTO);
                supplier.Status = true;
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return StatusCode(404, "Có lỗi xảy ra...");
            }
        }

        #endregion


    }
}
