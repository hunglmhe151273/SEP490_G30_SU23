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
            if(year == 0)
            {
                return BadRequest();
            }
            //tổng tiền nhận được trong tháng ... của năm

            //tổng tiền phải chi trong tháng ... của năm

            // Lợi thuận của tháng ... của năm

            //test hoạt động của json
            ChartByYearVM vm = new ChartByYearVM();
            if (year == 2021) {
                vm.Jan = 100000;
                vm.Feb = 200000;
                vm.Mar = 300000;
            }else if(year == 2022)
            {
                vm.Jan = 100000;
                vm.Feb = 200000;
                vm.Mar = 300000;
                vm.Apr = 400000;
                vm.May = 500000;
                vm.Jun = 600000;
            }
            else if (year == 2023)
            {
                vm.Jan = 100000;
                vm.Feb = 200000;
                vm.Mar = 300000;
                vm.Apr = 400000;
                vm.May = 500000;
                vm.Jun = 600000;
                vm.Jul = 700000;
                vm.Aug = 800000;
                vm.Sep = 900000;
            }

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DailyReport(string? selectValue)
        {
            if (string.IsNullOrEmpty(selectValue))
            {
                return BadRequest();
            }
            //tổng tiền nhận được trong tháng ... của năm

            //tổng tiền phải chi trong tháng ... của năm

            // Lợi thuận của tháng ... của năm

            //test hoạt động của json
            DailyReportVM vm = new DailyReportVM();
            if (selectValue.Equals("Hôm nay"))
            {
                vm.Revenue = 1;
                vm.NewOrder = 2;
                vm.DoneOrder = 3;
                vm.CancelledOrder = 4;
            }
            else if (selectValue.Equals("Tuần này"))
            {
                vm.Revenue = 5;
                vm.NewOrder = 6;
                vm.DoneOrder = 7;
                vm.CancelledOrder = 8;
            }
            else if (selectValue.Equals("Tháng này"))
            {
                vm.Revenue = 9;
                vm.NewOrder = 10;
                vm.DoneOrder = 11;
                vm.CancelledOrder = 12;
            }

            return Ok(vm);
        }
        #endregion
    }
}
