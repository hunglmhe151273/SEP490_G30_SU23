using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models.DTO;
using VBookHaven.Models;
using VBookHaven.Models.ViewModels;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBookHaven.Utility;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
    public class HomeController : Controller
    {

        private readonly VBookHavenDBContext _dbContext;
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        private readonly UserManager<IdentityUser> _userManager;
        IMapper _mapper;
        public HomeController(IMapper mapper, IApplicationUserRespository applicationUserRespository,
            UserManager<IdentityUser> userManager,VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
            //use
            _userManager = userManager;
            _IApplicationUserRespository = applicationUserRespository;
            _mapper = mapper;
        }
     
        public async Task<IActionResult> Dashboard()
        {
            var staffToView = await GetStaffAccountByUserID();
            var staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();
            DashboardVM vm = new DashboardVM();
            //set list year all order
            vm.DefaultYear = DateTime.Now.Year;
            vm.YearList = GetYearList(vm.DefaultYear);
            vm.TotalCustomer = _dbContext.Customers.Where(c => c.Status != false).Count();
            vm.TotalStaff = await getStaffQuantity();
            if (staffRole.Equals(SD.Role_Owner))
            {
                vm.TotalOrders = _dbContext.Orders.Where(o => o.Status.Equals(OrderStatus.Done)).Count();
                vm.TotalPurchaseOrders = _dbContext.PurchaseOrders.Where(o => o.Status.Equals(SD.PurchaseOrder_Complete)).Count();
            }
            else
            {
                vm.TotalOrders = _dbContext.Orders.Where(o => o.Status.Equals(OrderStatus.Done) && o.StaffId == staffId).Count();
                vm.TotalPurchaseOrders = _dbContext.PurchaseOrders.Where(o => o.Status.Equals(SD.PurchaseOrder_Complete) && o.StaffId == staffId).Count();
            }
           
            return View(vm);
        }

        //-------My Function-------
        private async Task<int> getStaffQuantity()
        {
            List<ApplicationUser> objUserList = await _IApplicationUserRespository.GetAllStaffAsync();
            foreach (var user in objUserList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            }
            objUserList = objUserList.Where(x => x.Role != SD.Role_Owner).ToList();
            return objUserList.Count;
        }

        private IEnumerable<SelectListItem> GetYearList(int defaultYear)
        {
            var yearWithDefaults = new List<SelectListItem>();

            var yearList = _dbContext.Orders
               .Where(o => o.OrderDate.HasValue) // Filter out records with null OrderDate
               .Select(o => o.OrderDate.Value.Year)
               .Distinct()
               .ToList();
            yearWithDefaults.AddRange(yearList.Select(year => new SelectListItem
            {
                Text = year.ToString(),
                Value = year.ToString()
            }));

            // Set the selected role as the default option
            foreach (var year in yearWithDefaults)
            {
                if (year.Value == defaultYear.ToString())
                {
                    year.Selected = true;
                    break;
                }
            }
            return yearWithDefaults;
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
        private async Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate)
        {
            var staffToView = await GetStaffAccountByUserID();
            var staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();


            if (staffRole.Equals(SD.Role_Owner))
            {
                return _dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null
                     && od.Order.Status == OrderStatus.Done
                    && od.Order.OrderDate.Value.Date >= startDate
                     && od.Order.OrderDate.Value.Date <= endDate)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0) * (1 + (od.Order.VAT ?? 0))));
            }
            else
            {
                return _dbContext.OrderDetails
                   .Where(od => od.Order.OrderDate != null
                    && od.Order.Status == OrderStatus.Done
                   && od.Order.OrderDate.Value.Date >= startDate
                    && od.Order.OrderDate.Value.Date <= endDate 
                    && od.Order.StaffId == staffId)
                   .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0) * (1 + (od.Order.VAT ?? 0))));
            }
        }
        private async Task<int> GetProcessOrder(DateTime startDate, DateTime endDate)
        {
            var staffToView = await GetStaffAccountByUserID();
            int staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();


            if (staffRole.Equals(SD.Role_Owner))
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status != OrderStatus.Done
                                && o.Status != OrderStatus.Cancel);
            }
            else
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status != OrderStatus.Done
                                && o.Status != OrderStatus.Cancel
                                && o.StaffId == staffId
                                );
            }
        }
        private async Task<int> GetDoneOrder(DateTime startDate, DateTime endDate)
        {
            var staffToView = await GetStaffAccountByUserID();
            int staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();


            if (staffRole.Equals(SD.Role_Owner))
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status == OrderStatus.Done);
            }
            else
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status == OrderStatus.Done
                                && o.StaffId == staffId
                                );
            }
        }
        private async Task<int> GetCancelledOrder(DateTime startDate, DateTime endDate)
        {
            var staffToView = await GetStaffAccountByUserID();
            int staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();


            if (staffRole.Equals(SD.Role_Owner))
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status == OrderStatus.Cancel);
            }
            else
            {
                return _dbContext.Orders
                    .Count(o => o.OrderDate != null
                                && o.OrderDate.Value.Date >= startDate
                                && o.OrderDate.Value.Date <= endDate
                                && o.Status == OrderStatus.Cancel
                                && o.StaffId == staffId
                                );
            }
        }
        private async Task<List<StaffDTO>> GetListStaffDTOWithRevenueAsync(DateTime startDate, DateTime endDate)
        {
    
          var listStaffs = await _dbContext.Staff.Include(s => s.Orders.Where(o => o.OrderDate.Value.Date >= startDate 
                                                                         && o.OrderDate.Value.Date <= endDate 
                                                                         && o.Status.Equals(OrderStatus.Done)))
                                                                            .ThenInclude(o => o.OrderDetails)
                                                                         .Where(s => s.StaffId != 1) //to do: find owner id
                                        .ToListAsync();

            return listStaffs.Select(_mapper.Map<Staff, StaffDTO>).ToList();
        }
        #region CallAPI
        [HttpGet]
        public async Task<IActionResult> Chart(int? year)
        {
            if (year == 0)
            {
                return BadRequest();
            }
            var staffToView = await GetStaffAccountByUserID();
            if (staffToView == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            
            //----------------------Doanh thu----------------------
            //Tổng doanh thu theo năm
            ChartByYearVM vm = new ChartByYearVM();
            vm =  await GetChartByYear(vm, year);
            return Ok(vm);
        }
        private async Task<ChartByYearVM> GetChartByYear(ChartByYearVM vm,int? year)
        {
                vm.Jan = await ToTalOrderPriceInMonthInYearAsync(1, year);
                vm.Feb = await ToTalOrderPriceInMonthInYearAsync(2, year);
                vm.Mar = await ToTalOrderPriceInMonthInYearAsync(3, year);
                vm.Apr = await ToTalOrderPriceInMonthInYearAsync(4, year);
                vm.May = await ToTalOrderPriceInMonthInYearAsync(5, year);
                vm.Jun = await ToTalOrderPriceInMonthInYearAsync(6, year);
                vm.Jul = await ToTalOrderPriceInMonthInYearAsync(7, year);
                vm.Aug = await ToTalOrderPriceInMonthInYearAsync(8, year);
                vm.Sep = await ToTalOrderPriceInMonthInYearAsync(9, year);
                vm.Oct = await ToTalOrderPriceInMonthInYearAsync(10, year);
                vm.Nov = await ToTalOrderPriceInMonthInYearAsync(11, year);
                vm.Dec = await ToTalOrderPriceInMonthInYearAsync(12, year);
            return vm;
        }
        private async Task<decimal?> ToTalOrderPriceInMonthInYearAsync(int month, int? year)
        {
            var staffToView = await GetStaffAccountByUserID();
            var staffId = staffToView.Staff.StaffId;
            string staffRole = _userManager.GetRolesAsync(staffToView).GetAwaiter().GetResult().FirstOrDefault();
            
            DateTime today = DateTime.Today;
            if(month > today.Month && year == today.Year) { return null; }

            if (staffRole.Equals(SD.Role_Owner))
            {
                return _dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null && od.Order.OrderDate.Value.Year == year
                    && od.Order.OrderDate.Value.Month == month && od.Order.Status == OrderStatus.Done)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
            }
            else
            {
                return _dbContext.OrderDetails
                  .Where(od => od.Order.OrderDate != null && od.Order.OrderDate.Value.Year == year
                  && od.Order.OrderDate.Value.Month == month && od.Order.Status == OrderStatus.Done && od.Order.Staff.StaffId == staffId)
                  .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
            }
        }
        [HttpPost]
        public async Task<IActionResult> DailyReport(string? selectValue)
        {
            var staffToView = await GetStaffAccountByUserID();
            if (staffToView == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (string.IsNullOrEmpty(selectValue))
            {
                return BadRequest();
            }
            DailyReportVM vm = new DailyReportVM();
            if (selectValue.Equals("Hôm nay"))
            {
                DateTime today = DateTime.Today;
                DateTime startDate = DateTime.Today;
                //Doanh thu hôm nay
                vm.Revenue = await GetRevenueAsync(startDate,today);
                //Đơn hàng cần xử lý
                vm.ProcessOrder = await GetProcessOrder(startDate, today);
                //Đơn hàng đã hoàn thành
                vm.DoneOrder = await GetDoneOrder(startDate, today);
                //Đơn hàng đã hủy
                vm.CancelledOrder = await GetCancelledOrder(startDate, today);
                //Danh sách nhân viên cùng doanh thu
                vm.StaffDTOs = await GetListStaffDTOWithRevenueAsync(startDate, today);
            }
            else if (selectValue.Equals("7 ngày"))
            {
                DateTime today = DateTime.Today;
                DateTime startDate = today.AddDays(-6);

                //Doanh thu hôm nay
                vm.Revenue = await GetRevenueAsync(startDate, today);
                //Đơn hàng cần xử lý
                vm.ProcessOrder = await GetProcessOrder(startDate, today);
                //Đơn hàng đã hoàn thành
                vm.DoneOrder = await GetDoneOrder(startDate, today);
                //Đơn hàng đã hủy
                vm.CancelledOrder = await GetCancelledOrder(startDate, today);
                //Danh sách nhân viên cùng doanh thu
                vm.StaffDTOs = await GetListStaffDTOWithRevenueAsync(startDate, today);
                //Danh sách nhân viên cùng doanh thu
                vm.StaffDTOs = await GetListStaffDTOWithRevenueAsync(startDate, today);
            }
            else if (selectValue.Equals("30 ngày"))
            {
                DateTime today = DateTime.Today;
                DateTime startDate = today.AddDays(-29);

                //Doanh thu hôm nay
                vm.Revenue = await GetRevenueAsync(startDate, today);
                //Đơn hàng cần xử lý
                vm.ProcessOrder = await GetProcessOrder(startDate, today);
                //Đơn hàng đã hoàn thành
                vm.DoneOrder = await GetDoneOrder(startDate, today);
                //Đơn hàng đã hủy
                vm.CancelledOrder = await GetCancelledOrder(startDate, today);
                //Danh sách nhân viên cùng doanh thu
                vm.StaffDTOs = await GetListStaffDTOWithRevenueAsync(startDate, today);
            }
            return Ok(vm);
        }
        #endregion


    }
}
