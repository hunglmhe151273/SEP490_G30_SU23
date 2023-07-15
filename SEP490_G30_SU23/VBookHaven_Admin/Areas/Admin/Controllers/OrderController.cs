using Microsoft.AspNetCore.Mvc;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
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
		public List<Product> Products { get; set; }
		public List<Customer> Customers { get; set; }
		public List<OrderDetail> Detail { get; set; }


		public AddOrderManagementModel()
		{
			Products = new List<Product>();
			Customers = new List<Customer>();
			Detail = new List<OrderDetail>();
		}
	}

	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderRepository orderRepository;
		private readonly IApplicationUserRespository userRespository;
		private readonly IProductRespository productRespository;

		public OrderController(IOrderRepository orderRepository, IApplicationUserRespository userRespository,
			IProductRespository productRespository)
		{
			this.orderRepository = orderRepository;
			this.userRespository = userRespository;
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
			var userTask = userRespository.GetAllCustomersAsync();
			var productTask = productRespository.GetAllProductsAsync();
			
			var model = new AddOrderManagementModel();
			
			return View();
		}
	}
}
