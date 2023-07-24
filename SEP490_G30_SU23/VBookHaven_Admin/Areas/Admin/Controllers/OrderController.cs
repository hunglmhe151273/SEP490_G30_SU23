using Microsoft.AspNetCore.Mvc;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
	// Chua co validate du lieu khi add don hang
	
	// Them anh thumbnail cho product khi add order?
	// Lua chon gia le - gia si khi lap don hang
	// Add don hang - chua co tong cong tien khach can tra
	
	public class ViewOrderManagementModel
	{
		public List<Order> Orders { get; set; }

		public ViewOrderManagementModel()
		{
			Orders = new List<Order>();
		}
	}

	public class AddOrderManagementModel
	{
		public List<Product> AllProducts { get; set; }
		public List<ShippingInfo> AllShipInfo { get; set; }

		public List<int> ProductIdList { get; set; }
		public List<int> QuantityList { get; set; }
		public List<int> PriceList { get; set; }
		public List<double> DiscountList { get; set; }

		public Order Order { get; set; }


		public AddOrderManagementModel()
		{
			AllProducts = new List<Product>();
			AllShipInfo = new List<ShippingInfo>();

			ProductIdList = new List<int>();
			QuantityList = new List<int>();
			PriceList = new List<int>();
			DiscountList = new List<double>();

			Order = new Order();
		}
	}

	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderRepository orderRepository;
		private readonly IShippingInfoRepository shippingInfoRepository;
		private readonly IProductRespository productRespository;

		public OrderController(IOrderRepository orderRepository, IShippingInfoRepository shippingInfoRepository,
			IProductRespository productRespository)
		{
			this.orderRepository = orderRepository;
			this.shippingInfoRepository = shippingInfoRepository;
			this.productRespository = productRespository;
		}

		public async Task<IActionResult> Index()
		{
			var model = new ViewOrderManagementModel();
			
			model.Orders = await orderRepository.GetAllOrdersFullInfoAsync();
			return View(model);
		}

		public async Task<IActionResult> Add()
		{
			var shipInfoTask = shippingInfoRepository.GetAllShipInfoAsync();
			var productTask = productRespository.GetAllProductsAsync();
			
			var model = new AddOrderManagementModel();
			model.AllShipInfo = await shipInfoTask;
			model.AllProducts = await productTask;
			
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddOrderManagementModel model)
		{
			model.Order.OrderDate = DateTime.Now;
			model.Order.Status = "Chờ xác nhận";

			var detailList = new List<OrderDetail>();
			for (int i = 0; i < model.ProductIdList.Count; ++i)
			{
				var detail = new OrderDetail
				{
					ProductId = model.ProductIdList[i],
					Quantity = model.QuantityList[i],
					UnitPrice = model.PriceList[i],
					Discount = model.DiscountList[i]
				};

				detailList.Add(detail);
			}

			await orderRepository.AddOrderAsync(model.Order, detailList);

			return RedirectToAction("Index");
		}
	}
}
