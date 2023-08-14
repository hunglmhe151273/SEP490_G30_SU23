using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models.DTO;
using VBookHaven.Models;
using VBookHaven.Models.ViewModels;
using System.Runtime.Intrinsics.X86;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShippingInfoRepository shippingInfoRepository;
        private readonly IProductRespository productRespository;
        private readonly IMapper mapper;
        private readonly IApplicationUserRespository userRepository;
        private readonly IImageRepository imageRepository;
        private readonly ICustomerRespository customerRespository;
        private readonly VBookHavenDBContext _dbContext;

        public HomeController(IOrderRepository orderRepository, IShippingInfoRepository shippingInfoRepository,VBookHavenDBContext dbContext,
            IProductRespository productRespository, IMapper mapper, IApplicationUserRespository userRepository,
            IImageRepository imageRepository, ICustomerRespository customerRespository)
        {
            _dbContext = dbContext;
            this.orderRepository = orderRepository;
            this.shippingInfoRepository = shippingInfoRepository;
            this.productRespository = productRespository;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
            this.customerRespository = customerRespository;
        }

        public async Task<IActionResult> Dashboard()
        {
            return View();
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
