using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Cryptography;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.Models.DTO;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseOrderController : Controller
    {
        private readonly VBookHavenDBContext _dbContext;
        IMapper _mapper;
        public PurchaseOrderController(IMapper mapper, VBookHavenDBContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #region CallAPI
        [HttpGet]
        public ActionResult<IEnumerable<SupplierDTO>> GetAllSuppliers()
        {
            //get list supplier and return list
            var listSuppliers = new List<Supplier>();
            try
            {
                listSuppliers = _dbContext.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listSuppliers.Select(_mapper.Map<Supplier, SupplierDTO>).ToList();
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAllProducts()
        {
            //get list product and return list
            var listProducts = new List<Product>();
            try
            {
                listProducts = _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts.Select(_mapper.Map<Product, ProductDTO>).ToList();
        }
        #endregion
    }
}
