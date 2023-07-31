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
    public class TestPO : Controller
    {
        private readonly VBookHavenDBContext _context;

        public TestPO(VBookHavenDBContext context)
        {
            _context = context;
        }

        // GET: Admin/TestPO
        public async Task<IActionResult> Index()
        {
            var vBookHavenDBContext = _context.PurchaseOrders.Include(p => p.Staff).Include(p => p.Supplier);
            return View(await vBookHavenDBContext.ToListAsync());
        }

        // GET: Admin/TestPO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: Admin/TestPO/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Address");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Phone");
            return View();
        }

        // POST: Admin/TestPO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,Date,StaffId,SupplierId,Status,Description,AmountPaid,VAT")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Address", purchaseOrder.StaffId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Phone", purchaseOrder.SupplierId);
            return View(purchaseOrder);
        }

        // GET: Admin/TestPO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Address", purchaseOrder.StaffId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Phone", purchaseOrder.SupplierId);
            return View(purchaseOrder);
        }

        // POST: Admin/TestPO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderId,Date,StaffId,SupplierId,Status,Description,AmountPaid,VAT")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PurchaseOrderId))
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
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Address", purchaseOrder.StaffId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Phone", purchaseOrder.SupplierId);
            return View(purchaseOrder);
        }

        // GET: Admin/TestPO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // POST: Admin/TestPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseOrders == null)
            {
                return Problem("Entity set 'VBookHavenDBContext.PurchaseOrders'  is null.");
            }
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder != null)
            {
                _context.PurchaseOrders.Remove(purchaseOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
          return (_context.PurchaseOrders?.Any(e => e.PurchaseOrderId == id)).GetValueOrDefault();
        }
    }
}
