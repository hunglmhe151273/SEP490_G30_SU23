using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBookHaven.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using VBookHaven.DataAccess.Respository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using AutoMapper;
using VBookHaven.Models.ViewModels;
using System.Security.Cryptography;
using VBookHaven.Models.DTO;
using System.Net.WebSockets;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CustomerController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        private readonly VBookHavenDBContext _dbContext;
        private readonly IShippingInfoRepository _shippingInfoRepository;
        IMapper _mapper;
        public CustomerController(IMapper mapper,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            //ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IShippingInfoRepository shippingInfoRepository,
            IApplicationUserRespository applicationUserRespository,
            IWebHostEnvironment webHostEnvironment,
            VBookHavenDBContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _signInManager = signInManager;
            //_logger = logger;
            _emailSender = emailSender;
            _IApplicationUserRespository = applicationUserRespository;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _shippingInfoRepository = shippingInfoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _dbContext.Customers.Include(p => p.Orders).ToListAsync();
            //foreach(var cus in customers)
            //{
            //    int count = cus.Orders.Count();
            //}
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manage_CreateCustomerVM model)
        {
            ModelState.Remove("Customer.DefaultShippingInfo.CustomerName");
            if (ModelState.IsValid)
            {
                model.Customer.Phone = model.Customer.DefaultShippingInfo.Phone;
                model.Customer.DefaultShippingInfo.CustomerName = model.Customer.FullName;
                var customer = model.Customer;
                customer.Status = true;
                _dbContext.Add(customer);
                await _dbContext.SaveChangesAsync();
                //string wwwRootPath = _webHostEnvironment.WebRootPath;
                ////sau khi add vao db
                //if (model.Customer_ImageFile != null)
                //{
                //    string fileName = customer.CustomerId.ToString() + Path.GetExtension(model.Customer_ImageFile.FileName);
                //    string customerPath = Path.Combine(wwwRootPath, @"images\customer");

                //    using (var fileStream = new FileStream(Path.Combine(customerPath, fileName), FileMode.Create))
                //    {
                //        model.Customer_ImageFile.CopyTo(fileStream);
                //    }

                //    customer.Image = @"\images\customer\" + fileName;
                //}

                customer.DefaultShippingInfo.CustomerId = customer.CustomerId;
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Tạo khách hàng thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
            //Tat ca don hang bao gom tat ca cac trang thai don hang
            var orders = await _dbContext.Orders.Include(o => o.Staff).Include(o => o.OrderDetails).Where(c => c.CustomerId == id).ToListAsync();
            var shippingInfos = await _dbContext.ShippingInfos.Include(c => c.Customers).Where(c => c.CustomerId == id).ToListAsync();

            DetailsCustomerVM model = new DetailsCustomerVM();
            if (customer != null)
                model.Customer = customer;
            if (orders != null)
            {
                model.OrderDTOs = orders.Select(_mapper.Map<Order, OrderDTO>).ToList();
                //tong so don hang bao gom tat ca cac trang thai
                model.totalOrderQuantity = orders.Count();
                //So san pham da mua trong nhung don hang da hoan thanh
                var details = orders.Where(o => o.Status.Equals(OrderStatus.Done)).SelectMany(d => d.OrderDetails).ToList();
                model.totalBuyProduct = totalBuyProductFunc(details);
            }
            if (shippingInfos != null)
                model.ShippingInfos = shippingInfos;
            return View(model);
        }
        private int totalBuyProductFunc(List<OrderDetail> details)
        {
            int total = 0;
            foreach (var d in details)
            {
                total = (int)(total + d.Quantity);
            }
            return total;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VBookHaven.Models.Customer cus)
        {
            if (ModelState.IsValid)
            {
                var customer = await _dbContext.Customers.Include(c => c.Account).SingleOrDefaultAsync(c => c.CustomerId == cus.CustomerId);

                if (customer != null && customer.Account != null)
                {
                    var userFromDb = await _dbContext.ApplicationUsers.SingleOrDefaultAsync(a => a.Id.Equals(customer.Account.Id));
                    if (cus.Status == false)
                    {
                        userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                    }
                    else if (cus.Status == true && userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
                    {
                        //user is currently locked and we need to unlock them
                        userFromDb.LockoutEnd = DateTime.Now;
                    }
                }
                if (customer != null)
                {
                    customer.FullName = cus.FullName;
                    customer.Phone = cus.Phone;
                    customer.DOB = cus.DOB;
                    customer.IsWholesale = cus.IsWholesale;
                    customer.IsMale = cus.IsMale;
                    customer.Status = cus.Status;
                    await _dbContext.SaveChangesAsync();
                    TempData["success"] = "Cập nhật khách hàng thành công";
                    return RedirectToAction("Details", new { id = customer.CustomerId });
                }
            }
            //TempData["error"] = "Cập nhật chưa thành công";
            return View(cus);
        }
        [HttpGet]
        public async Task<IActionResult> CreateShipInfo(int cid)
        {
            if (cid == 0) return RedirectToAction(nameof(Index));
            //lấy customer bằng id
            var customer = await _dbContext.Customers.Include(c => c.Account).SingleOrDefaultAsync(c => c.CustomerId == cid);
            //view customer lên cùng shipping info
            Manage_ShippingInfoVM model = new Manage_ShippingInfoVM();
            model.Customer = customer;
            //trả về view
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShipInfo(Manage_ShippingInfoVM model)
        {
            ModelState.Clear();
            if (!TryValidateModel(model.ShippingInfo))
            {
                return View(model);
            }
            //cập nhật lại customer shipping info
            var customer = await _dbContext.Customers.Include(c => c.Account).SingleOrDefaultAsync(c => c.CustomerId == model.Customer.CustomerId);
            if (customer != null)
                customer.ShippingInfos.Add(model.ShippingInfo);
            TempData["success"] = "Thêm địa chỉ khách hàng thành công";
            _dbContext.SaveChanges();
            return RedirectToAction("Details", new { id = customer.CustomerId });
        }
        [HttpGet]
        public async Task<IActionResult> EditShipInfo(int shipId)
        {
            if (shipId == 0) return RedirectToAction(nameof(Index));
            //lấy shipinfo bằng id
            var shippingInfo = await _dbContext.ShippingInfos.Include(c => c.Customer).SingleOrDefaultAsync(c => c.ShipInfoId == shipId);
       
            //view customer cùng shipping info
            Manage_ShippingInfoVM model = new Manage_ShippingInfoVM();
            if (shippingInfo != null)
            {
                model.Customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.CustomerId == shippingInfo.Customer.CustomerId);
                model.ShippingInfo = shippingInfo;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShipInfo(Manage_ShippingInfoVM model)
        {
            ModelState.Clear();
            if (!TryValidateModel(model.ShippingInfo))
            {
                return View(model);
            }
            //cập nhật shipping info
            var shippingInfo = await _dbContext.ShippingInfos.Include(c => c.Customer).SingleOrDefaultAsync(c => c.ShipInfoId == model.ShippingInfo.ShipInfoId);
            if (shippingInfo != null)
            {
                shippingInfo.CustomerName = model.ShippingInfo.CustomerName;
                shippingInfo.Phone = model.ShippingInfo.Phone;
                shippingInfo.ShipAddress = model.ShippingInfo?.ShipAddress;
                shippingInfo.Province = model.ShippingInfo?.Province;
                shippingInfo.District = model.ShippingInfo?.District;
                shippingInfo.Ward = model.ShippingInfo?.Ward;
                shippingInfo.WardCode = model.ShippingInfo?.WardCode;
                shippingInfo.DistrictCode = model.ShippingInfo?.DistrictCode;
                shippingInfo.ProvinceCode = model.ShippingInfo?.ProvinceCode;
                await _dbContext.SaveChangesAsync();
            }
            TempData["success"] = "Sửa địa chỉ khách hàng thành công";
            //To do: redirect den khach hang
            return RedirectToAction("Details", new { id = shippingInfo.CustomerId });
        }
        #region CallAPI
        [HttpGet]
        public async Task<IActionResult> DeleteShipInfo(int shipInfoId)
        {
            var shippingInfo = await _dbContext.ShippingInfos.Include(c => c.Customers).SingleOrDefaultAsync(c => c.ShipInfoId == shipInfoId);
            int cusID = (int)shippingInfo.CustomerId;
            //kiểm tra shipinfo có list cus? - kiểm tra default shippingInfo
            if (shippingInfo != null && shippingInfo.Customers.Count == 0)
            {
                await _shippingInfoRepository.DeleteShipInfoAsync(shippingInfo);
                TempData["success"] = "Xóa địa chỉ thành công";
                return RedirectToAction("Details", new { id = cusID });
            }
            //return StatusCode(400, new { Message = "Không thể xóa địa chỉ mặc định"});
            TempData["error"] = "Không thể xóa địa chỉ mặc định";
            return RedirectToAction("Details", new { id = cusID });
        }
        #endregion
    }
}
