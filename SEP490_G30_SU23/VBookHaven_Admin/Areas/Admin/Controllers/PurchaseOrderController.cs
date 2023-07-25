using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using System.Security.Cryptography;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Models.ViewModels;
using VBookHaven.ViewModels;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseOrderController : Controller
    {
        private readonly VBookHavenDBContext _dbContext;
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        private readonly IProductRespository _productRespository;
        IMapper _mapper;
        public PurchaseOrderController(IMapper mapper, IApplicationUserRespository applicationUserRespository, VBookHavenDBContext dbContext, IProductRespository productRespository)
        {
            _productRespository = productRespository;
            _mapper = mapper;
            _dbContext = dbContext;
            _IApplicationUserRespository = applicationUserRespository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseOrderVM model)
        {
            var staffCreate = await GetStaffByUserID();
            if (!ModelState.IsValid || staffCreate == null || model.ProductIdList.Count == 0)
            {
                TempData["error"] = "Thêm đơn nhập thất bại";
                return View();
            }
            PurchaseOrder pO = new PurchaseOrder();
            pO.Status = "Tạo đơn hàng";
            pO.Description = model.Note;
            pO.SupplierId = model.SupplierID;
            //thêm VAT
            pO.VAT = model.VAT;
            pO.Staff = staffCreate;
            //pO.AmountPaid = model.AmountPaid;
            for (int i = 0; i < model.ProductIdList.Count; ++i)
            {
                var detail = new PurchaseOrderDetail
                {
                    ProductId = model.ProductIdList[i],
                    Quantity = model.QuantityList[i],
                    UnitPrice = model.UnitPriceList[i],
                    Discount = model.DiscountList[i],
                };
                pO.PurchaseOrderDetails.Add(detail);
            }
            await _dbContext.PurchaseOrders.AddAsync(pO);
            _dbContext.SaveChanges();
            TempData["success"] = "Thêm đơn nhập thành công";
            return RedirectToAction(nameof(Create));
        }

        private async Task<Staff> GetStaffByUserID()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get application user by id
                var appUser = await _IApplicationUserRespository.GetStaffByUIdAsync(userId);//lấy ra các thông tin liên quan đến user bằng userID(Application là bảng User)
                //view application user
                return appUser.Staff;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region CallAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAllSuppliers()
        {
            //get list supplier and return list
            var listSuppliers = new List<Supplier>();
            try
            {
                listSuppliers = await _dbContext.Suppliers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listSuppliers.Select(_mapper.Map<Supplier, SupplierDTO>).ToList();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            //get list product and return list
            var listProducts = new List<Product>();
            try
            {
                listProducts = await _dbContext.Products.Include(x => x.Images).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts.Select(_mapper.Map<Product, ProductDTO>).ToList();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404, "Lỗi xảy ra khi thêm sản phẩm mới! Vui lòng thử lại sau.");
            }
            try
            {
                var product = _mapper.Map<ProductDTO, Product>(productDTO);
                product.Status = true;
                _dbContext.Products.Add(product);
                
                await _dbContext.SaveChangesAsync();
                if (string.IsNullOrEmpty(productDTO.Barcode))
                {
                    product.Barcode = "PVN" + product.ProductId;
                }

                if (productDTO.IsBook)
                {
                    Book book = new Book();
                    book.ProductId = product.ProductId;
                    _dbContext.Books.Add(book);
                }
                else
                {
                    Stationery stationery = new Stationery();
                    stationery.ProductId = product.ProductId;
                    _dbContext.Stationeries.Add(stationery);
                }
                await _dbContext.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(404, "Có lỗi thêm product xảy ra......");
            }
        }
        #endregion
    }
}
