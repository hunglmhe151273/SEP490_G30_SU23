using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Models.ViewModels;
using VBookHaven.Utility;
using VBookHaven.ViewModels;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
    public class PurchaseOrderController : Controller
    {
        private readonly VBookHavenDBContext _dbContext;
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        private readonly IProductRespository _productRespository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly UserManager<IdentityUser> _userManager;
        IMapper _mapper;
        [ActivatorUtilitiesConstructor]
        public PurchaseOrderController(IMapper mapper, IApplicationUserRespository applicationUserRespository,
            UserManager<IdentityUser> userManager,
            VBookHavenDBContext dbContext, IProductRespository productRespository, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _productRespository = productRespository;
            _mapper = mapper;
            _dbContext = dbContext;
            _IApplicationUserRespository = applicationUserRespository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _userManager = userManager;
        }

        public PurchaseOrderController()
        {
        }

        public async Task<IActionResult> Index()
        {
            var staffToView = await GetStaffAccountByUserID();
            if(staffToView == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            var staffId = staffToView.Staff.StaffId;
            string userRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();
            
            if (userRole.Equals(SD.Role_Owner))
            {
                var purchaseOrders = await _dbContext.PurchaseOrders
                                            .Include(p => p.Staff).Include(p => p.Supplier).ToListAsync();
                return View(purchaseOrders);
            }
            else
            {
                var purchaseOrders = await _dbContext.PurchaseOrders.Where(p => p.StaffId == staffId)
                                             .Include(p => p.Staff).Include(p => p.Supplier).ToListAsync();
                return View(purchaseOrders);
            }
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
            if (staffCreate == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            ModelState.Remove("PurchaseOrderEdit");
            if (!ModelState.IsValid || staffCreate == null || model.ProductIdList.Count == 0)
            {
                TempData["error"] = "Thêm đơn nhập thất bại";
                return View(model);
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
            return RedirectToAction("Details", new { id = pO.PurchaseOrderId });
        }
        public async Task<IActionResult> Edit(int purchaseId)
        {
            var staffEdit = await GetStaffByUserID();
            if (purchaseId == 0 || _dbContext.PurchaseOrders == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var purchaseOrder = await _dbContext.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .Include(p => p.PurchasePaymentHistories)
                .Include(p => p.PurchaseOrderDetails).ThenInclude(d => d.Product).ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == purchaseId);
            if (purchaseOrder == null || SD.POStatusToNum(purchaseOrder.Status) > 1)
            {
                return RedirectToAction(nameof(Index));
            }
            //front end cần total paid để so sánh với tổng giá trị đơn hàng, không được phép nhỏ hơn
            CreatePurchaseOrderVM vm = new CreatePurchaseOrderVM();
            //vm.TotalPayment = totalPaymentNotVAT(purchaseOrder.PurchaseOrderDetails)*(1 + (decimal)purchaseOrder.VAT/100);
            vm.PurchaseOrderEdit = purchaseOrder;
            vm.DefaultSupplierIDShow = purchaseOrder.Supplier.SupplierId;
            vm.TotalPaid = totalPaid(purchaseOrder.PurchasePaymentHistories);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreatePurchaseOrderVM model)
        {
            if(model.TotalPayment < model.TotalPaid) {
                TempData["error"] = "Sửa đơn nhập thất bại";
                return View(model);
            }
            var staffCreate = await GetStaffByUserID();
            if (!ModelState.IsValid || staffCreate == null || model.ProductIdList.Count == 0)
            {
                TempData["error"] = "Sửa đơn nhập thất bại";
                return View(model);
            }
            //Nếu không thay đổi supplier
            if(model.PurchaseOrderEdit.SupplierId == 0 || model.PurchaseOrderEdit.SupplierId == null) {
                model.PurchaseOrderEdit.SupplierId = model.DefaultSupplierIDShow;
            }
            var pO = await _dbContext.PurchaseOrders.Include(p=> p.PurchaseOrderDetails).SingleOrDefaultAsync(po => po.PurchaseOrderId == model.PurchaseOrderEdit.PurchaseOrderId);
           
            if (pO != null) {
                pO.PurchaseOrderDetails.Clear();
                pO.Status = "Tạo đơn hàng";
                pO.Description = model.PurchaseOrderEdit.Description;
                pO.SupplierId = model.PurchaseOrderEdit.SupplierId;
                pO.Date = DateTime.Now;
                pO.VAT = model.PurchaseOrderEdit.VAT;
                pO.Staff = staffCreate;
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
            }
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Cập nhật đơn hàng nhập thành công";
            return RedirectToAction("Details", new { id = pO.PurchaseOrderId });
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
        private async Task<ApplicationUser> GetStaffAccountByUserID()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get application user by id
                var appUser = await _IApplicationUserRespository.GetStaffByUIdAsync(userId);//lấy ra các thông tin liên quan đến user bằng userID(Application là bảng User)
                //view application user
                return appUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            var staffEdit = await GetStaffByUserID();
            if (id == null || _dbContext.PurchaseOrders == null)
            {
                return BadRequest();
            }
            var purchaseOrder = await _dbContext.PurchaseOrders
                .Include(p => p.Staff)
                .Include(p => p.Supplier)
                .Include(p => p.PurchasePaymentHistories)
                .Include(p => p.PurchaseOrderDetails).ThenInclude(d => d.Product).ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return BadRequest();
            }
			if (purchaseOrder.StaffId != staffEdit.StaffId)
			{
				return BadRequest();
			}
			//Tổng tiền hàng
			decimal totalForPayment = Math.Ceiling((decimal)totalPaymentNotVAT(purchaseOrder.PurchaseOrderDetails) * (1 + (decimal)purchaseOrder.VAT / 100));
            //Tổng đã trả
            decimal sumPaid = Math.Ceiling((decimal)totalPaid(purchaseOrder.PurchasePaymentHistories));
            //Tính số tiền còn thiếu
            DetailsPurchaseOrderVM vm = new DetailsPurchaseOrderVM();
            vm.Unpaid = Math.Ceiling(totalForPayment - sumPaid);
            vm.TotalPayment = totalForPayment;
            vm.Paid = sumPaid;
            vm.pO = purchaseOrder;
            //Form
            vm.PPHistory.PurchaseId = purchaseOrder.PurchaseOrderId;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var purchaseOrder = await _dbContext.PurchaseOrders.SingleOrDefaultAsync(o => o.PurchaseOrderId == id);
            if (purchaseOrder == null)
                return RedirectToAction("Index", "PurchaseOrder");

            var staff = await GetStaffByUserID();
            if (staff == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            switch (purchaseOrder.Status)
            {
                case (SD.PurchaseOrder_Created):
                    purchaseOrder.Status = SD.PurchaseOrder_Imported;
                    TempData["success"] = "Cập nhật đơn hàng thành công";
                    //Thêm nhân viên cập nhật nếu cần
                    await _purchaseOrderRepository.UpdatePurchaseOrderAsync(purchaseOrder);
                    await ImportToStorage(id);
                    break;
                //Chú ý khi làm tính năng thanh toán
                case (SD.PurchaseOrder_Imported):
                    purchaseOrder.Status = SD.PurchaseOrder_Complete;
                    TempData["success"] = "Cập nhật đơn hàng thành công";
                    //Thêm nhân viên cập nhật nếu cần
                    await _purchaseOrderRepository.UpdatePurchaseOrderAsync(purchaseOrder);
                    break;
            }
            return RedirectToAction("Details", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CancelPurchaseOrder(int id)
        {
            var purchaseOrder = await _dbContext.PurchaseOrders.SingleOrDefaultAsync(o => o.PurchaseOrderId == id);
            if (purchaseOrder == null)
                return NotFound();

            var staff = await GetStaffByUserID();
            if (staff == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (purchaseOrder.Status.Equals(SD.PurchaseOrder_Created))
            {
                purchaseOrder.Status = SD.PurchaseOrder_Canceled;
                await _purchaseOrderRepository.UpdatePurchaseOrderAsync(purchaseOrder);
            }
            
            return RedirectToAction("Details", new { id = id });
        }
        private async Task ImportToStorage(int purchaseOrderId)
        {
            var purchaseOrder = await _dbContext.PurchaseOrders.Include(p => p.PurchaseOrderDetails)
                                                                .ThenInclude(d => d.Product)
                                                                .SingleOrDefaultAsync(o => o.PurchaseOrderId == purchaseOrderId);
            if (purchaseOrder == null)
                RedirectToAction("Index", "PurchaseOrder");

            foreach (var detail in purchaseOrder.PurchaseOrderDetails)
            {
                var product = await _dbContext.Products.SingleOrDefaultAsync(o => o.ProductId == detail.ProductId);
                decimal newPurchasePrice = (decimal)(detail.UnitPrice * (1 - (decimal)(detail.Discount / 100)) * (1 + (decimal)purchaseOrder.VAT / 100));
                decimal totalNewPrice = (decimal)(newPurchasePrice * detail.Quantity);
                decimal totalOldPrice = (decimal)(product.PurchasePrice * product.UnitInStock);
                decimal totalInStock = (decimal)((decimal)product.UnitInStock + detail.Quantity);

                product.PurchasePrice = Math.Ceiling((totalNewPrice + totalOldPrice) / totalInStock);
                product.UnitInStock += detail.Quantity;
                product.AvailableUnit += detail.Quantity;
                await _productRespository.UpdateProductAsync(product);
            }
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
        
        // Tổng tiền của đơn hàng - tham số purchasorder.purchaseOrderDetails
        private decimal? totalPaymentNotVAT(ICollection<PurchaseOrderDetail> purchaseOrderDetails)
        {
            decimal sum = 0;
            foreach (var detail in purchaseOrderDetails)
            {
                if (detail.Quantity.HasValue && detail.UnitPrice.HasValue && detail.Discount.HasValue)
                {
                    sum += (decimal)(detail.Quantity * detail.UnitPrice * (decimal)(1 - detail.Discount/100));
                }
            }
            return sum;
        }
        //Tổng đã trả - tham số purchasorder.purchasePaymentHistories
        private decimal? totalPaid(ICollection<PurchasePaymentHistory> purchasePaymentHistories)
        {
            decimal sumPaid = 0;
            foreach (var history in purchasePaymentHistories)
            {
                if (history.PaymentAmount.HasValue)
                {
                    sumPaid += (decimal)(history.PaymentAmount);
                }
            }
            return sumPaid;
        }
        #region CallAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAllSuppliers()
        {
            //get list supplier and return list
            var listSuppliers = new List<Supplier>();
            try
            {
                listSuppliers = await _dbContext.Suppliers.Where(s => s.Status != false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listSuppliers.Select(_mapper.Map<Supplier, SupplierDTO>).ToList();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetAllSubCategories()
        {
            var subCategories = new List<SubCategory>();
            try
            {
                subCategories = await _dbContext.SubCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(subCategories.Select(_mapper.Map<SubCategory, SubCategoryDTO>).ToList());
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            //get list product and return list
            var listProducts = new List<Product>();
            try
            {
                listProducts = await _dbContext.Products.Include(x => x.Images).Where(p => p.Status != false).ToListAsync();
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
                listProducts = await _dbContext.Products.Where(p => !p.PurchaseOrderDetails.Where(pd => pd.PurchaseOrderId == purchaseId).Any() && p.Status != false).Include(x => x.Images).ToListAsync();
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
            //To Do: Validate unique barcode
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
                product.UnitInStock = 0;
                product.AvailableUnit = 0;

                _dbContext.Products.Add(product);
                
                await _dbContext.SaveChangesAsync();
                if (string.IsNullOrEmpty(productDTO.Barcode))
                {
                    product.Barcode = "PVN" + product.ProductId;
                }

                //check subcategory isBook or vpp
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
                return Ok(_mapper.Map<Product, ProductDTO>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(404, "Có lỗi thêm product xảy ra......");
            }
        }
        #endregion
    }
}
