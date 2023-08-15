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

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly VBookHavenDBContext _dbContext;
        //use
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(IApplicationUserRespository applicationUserRespository,
            UserManager<IdentityUser> userManager,VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
            //use
            _userManager = userManager;
            _IApplicationUserRespository = applicationUserRespository;
        }
     
        public async Task<IActionResult> Dashboard()
        {
            DashboardVM vm = new DashboardVM();
            //set list year all order
            vm.DefaultYear = DateTime.Now.Year;
            vm.YearList = GetYearList(vm.DefaultYear);
            vm.TotalCustomer = _dbContext.Customers.Where(c => c.Status != false).Count();
            vm.TotalOrders = _dbContext.Orders.Where(o => o.Status.Equals(OrderStatus.Done)).Count();
            vm.TotalPurchaseOrders = _dbContext.PurchaseOrders.Where(o => o.Status.Equals(SD.PurchaseOrder_Complete)).Count();
            vm.TotalStaff = await getStaffQuantity();
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

        #region CallAPI
        [HttpGet]
        public async Task<IActionResult> Chart(int? year)
        {
            if (year == 0)
            {
                return BadRequest();
            }
            //----------------------Doanh thu----------------------
            //Tổng doanh thu theo năm
            ChartByYearVM vm = new ChartByYearVM();
            vm = GetChartByYear(vm, year);
            return Ok(vm);
        }
        private ChartByYearVM GetChartByYear(ChartByYearVM vm,int? year)
        {
            vm.Jan = ToTalOrderPriceInMonthInYear(1, year);
            vm.Feb = ToTalOrderPriceInMonthInYear(2, year);
            vm.Mar = ToTalOrderPriceInMonthInYear(3, year);
            vm.Apr = ToTalOrderPriceInMonthInYear(4, year);
            vm.May = ToTalOrderPriceInMonthInYear(5, year);
            vm.Jun = ToTalOrderPriceInMonthInYear(6, year);
            vm.Jul = ToTalOrderPriceInMonthInYear(7, year);
            vm.Aug = ToTalOrderPriceInMonthInYear(8, year);
            vm.Sep = ToTalOrderPriceInMonthInYear(9, year);
            vm.Oct = ToTalOrderPriceInMonthInYear(10, year);
            vm.Nov = ToTalOrderPriceInMonthInYear(11, year);
            vm.Dec = ToTalOrderPriceInMonthInYear(12, year);
            return vm;
        }
        private decimal ToTalOrderPriceInMonthInYear(int month, int? year)
        {
            return (decimal)_dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null && od.Order.OrderDate.Value.Year == year && od.Order.OrderDate.Value.Month == month && od.Order.Status == OrderStatus.Done)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
        }
        [HttpPost]
        public async Task<IActionResult> DailyReport(string? selectValue)
        {
            if (string.IsNullOrEmpty(selectValue))
            {
                return BadRequest();
            }
            //Test hoạt động của json
            DailyReportVM vm = new DailyReportVM();
            if (selectValue.Equals("Hôm nay"))
            {
                DateTime today = DateTime.Today;
                //Doanh thu hôm nay
                vm.Revenue =  _dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null && od.Order.OrderDate.Value.Date == today)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
                //Đơn hàng cần xử lý
                vm.NewOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date == today 
                                && o.Status != OrderStatus.Done 
                                && o.Status != OrderStatus.Cancel);
                //Đơn hàng đã hoàn thành
                vm.DoneOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null && o.OrderDate.Value.Date == today && o.Status == OrderStatus.Done);
                //Đơn hàng đã hủy
                vm.CancelledOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null && o.OrderDate.Value.Date == today && o.Status == OrderStatus.Cancel);
            }
            else if (selectValue.Equals("7 ngày"))
            {
                DateTime today = DateTime.Today;
                DateTime last7Days = today.AddDays(-6);

                vm.Revenue = _dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null && od.Order.OrderDate.Value.Date >= last7Days 
                                                            && od.Order.OrderDate.Value.Date <= today)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
                vm.NewOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date >= last7Days 
                                && o.OrderDate.Value.Date <= today
                                && o.Status != OrderStatus.Done 
                                && o.Status != OrderStatus.Cancel);
                vm.DoneOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date >= last7Days
                                && o.OrderDate.Value.Date <= today
                                && o.Status == OrderStatus.Done);
                vm.CancelledOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                            && o.OrderDate.Value.Date >= last7Days
                            && o.OrderDate.Value.Date <= today
                            && o.Status == OrderStatus.Cancel);
            }
            else if (selectValue.Equals("30 ngày"))
            {
                DateTime today = DateTime.Today;
                DateTime last30Days = today.AddDays(-29);

                vm.Revenue = _dbContext.OrderDetails
                    .Where(od => od.Order.OrderDate != null 
                                && od.Order.OrderDate.Value.Date >= last30Days
                                && od.Order.OrderDate.Value.Date <= today)
                    .Sum(od => (decimal)(od.UnitPrice * (1 - (od.Discount ?? 0) / 100) * (od.Quantity ?? 0)));
                vm.NewOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date >= last30Days
                                && o.OrderDate.Value.Date <= today
                                && o.Status != OrderStatus.Done
                                && o.Status != OrderStatus.Cancel);
                vm.DoneOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date >= last30Days
                                && o.OrderDate.Value.Date <= today
                                && o.Status == OrderStatus.Done);
                vm.CancelledOrder = _dbContext.Orders
                    .Count(o => o.OrderDate != null 
                                && o.OrderDate.Value.Date >= last30Days
                                && o.OrderDate.Value.Date <= today
                                && o.Status == OrderStatus.Cancel);
            }

            return Ok(vm);
        }
        #endregion
    }
}
