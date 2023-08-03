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
            var vBookHavenDBContext = _dbContext.PurchaseOrders.Include(p => p.Staff).Include(p => p.Supplier);
            return View(await vBookHavenDBContext.ToListAsync());
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
            ModelState.Remove("PurchaseOrderEdit");
            if (!ModelState.IsValid || staffCreate == null || model.ProductIdList.Count == 0)
            {
                TempData["error"] = "Thêm đơn nhập thất bại";
                return View();
            }
            PurchaseOrder pO = new PurchaseOrder();
            pO.Status = "Tạo đơn hàng";
            pO.Description = model.Note;
            pO.SupplierId = model.SupplierID;
            pO.Date = DateTime.Now;
            pO.VAT = model.VAT;
            pO.Staff = staffCreate;
            // To do deltete pO.AmountPaid
            if (model.AmountPaid > 0)
            {
                PurchasePaymentHistory history = new PurchasePaymentHistory();
                history.PaymentDate = DateTime.Now;
                history.PaymentAmount = model.AmountPaid;
                history.Staff = staffCreate;
                pO.PurchasePaymentHistories.Add(history);
            }

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
        // GET: Admin/TestPO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var staffEdit = await GetStaffByUserID();
            if (id == null || _dbContext.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _dbContext.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .Include(p => p.PurchasePaymentHistories)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            
            //Tổng tiền hàng
            decimal sum  = 0;
            foreach(var detail in purchaseOrder.PurchaseOrderDetails)
            {
                if (detail.Quantity.HasValue && detail.UnitPrice.HasValue && detail.Discount.HasValue)
                {
                    sum += (decimal)(detail.Quantity * detail.UnitPrice * (decimal)(1 - detail.Discount));
                }
            }
            //Tổng đã trả
            decimal sumPaid = 0;
            foreach (var history in purchaseOrder.PurchasePaymentHistories)
            {
                if (history.PaymentAmount.HasValue)
                {
                    sumPaid += (decimal)(history.PaymentAmount);
                }
            }
            //Tính số tiền còn thiếu


            DetailsPurchaseOrderVM vm = new DetailsPurchaseOrderVM();
            vm.Unpaid = sum - sumPaid;
            vm.pO = purchaseOrder;
            //Form
            vm.PPHistory.PurchaseId = purchaseOrder.PurchaseOrderId;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PurchasePayment(DetailsPurchaseOrderVM model)
        {
            ModelState.Clear();
            if (!TryValidateModel(model.PPHistory))
            {
                TempData["error"] = "Cập nhật thanh toán thất bại";
                // view application user
                return View(model);
            }
            PurchasePaymentHistory payment = new PurchasePaymentHistory();
            var staffEdit = await GetStaffByUserID();
            payment.StaffId = staffEdit.StaffId;
            payment.PurchaseId = model.PPHistory.PurchaseId;
            payment.PaymentDate = model.PPHistory.PaymentDate;
            payment.PaymentAmount = model.PPHistory.PaymentAmount;

            await _dbContext.PurchasePaymentHistories.AddAsync(payment);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int purchaseId)
        {
            var staffEdit = await GetStaffByUserID();
            if (purchaseId == 0 || _dbContext.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _dbContext.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .Include(p => p.PurchasePaymentHistories)
                .Include(p => p.PurchaseOrderDetails).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == purchaseId);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            ////Tổng tiền hàng
            //decimal sum = 0;
            //foreach (var detail in purchaseOrder.PurchaseOrderDetails)
            //{
            //    if (detail.Quantity.HasValue && detail.UnitPrice.HasValue && detail.Discount.HasValue)
            //    {
            //        sum += (decimal)(detail.Quantity * detail.UnitPrice * (decimal)(1 - detail.Discount));
            //    }
            //}
            ////Tổng đã trả
            //decimal sumPaid = 0;
            //foreach (var history in purchaseOrder.PurchasePaymentHistories)
            //{
            //    if (history.PaymentAmount.HasValue)
            //    {
            //        sumPaid += (decimal)(history.PaymentAmount);
            //    }
            //}
            ////Tính số tiền còn thiếu
            //DetailsPurchaseOrderVM vm = new DetailsPurchaseOrderVM();
            //vm.Unpaid = sum - sumPaid;
            //vm.pO = purchaseOrder;
            ////Form
            //vm.PPHistory.PurchaseId = purchaseOrder.PurchaseOrderId;

            CreatePurchaseOrderVM vm = new CreatePurchaseOrderVM();
            vm.PurchaseOrderEdit = purchaseOrder;
            return View(vm);
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllOtherProductsByPurchaseId(int purchaseId)
        {
            if(purchaseId == 0)
            {
                return NotFound();
            }
            //get list product and return list
            var listProducts = new List<Product>();
            try
            {
                listProducts = await _dbContext.Products.Where(p => !p.PurchaseOrderDetails.Where(pd => pd.PurchaseOrderId == purchaseId).Any()).Include(x => x.Images).ToListAsync();
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
                product.RetailPrice = 0;
                product.WholesalePrice = 0;
                product.WholesaleDiscount = 0;
                product.RetailDiscount = 0;
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
