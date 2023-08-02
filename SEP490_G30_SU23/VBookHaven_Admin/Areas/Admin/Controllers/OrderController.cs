using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
	// Chua kiem tra da chon khach hang, da chon it nhat 1 san pham chua khi add don hang
	// Chua co validate du lieu khi add don hang (?)

	// Show thong tin staff xu li order?

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
		//public List<Product> AllProducts { get; set; }
		//public List<ShippingInfo> AllShipInfo { get; set; }

		public List<int> ProductIdList { get; set; }
		public List<int> QuantityList { get; set; }
		public List<int> PriceList { get; set; }
		public List<double> DiscountList { get; set; }

		public Order Order { get; set; }


		public AddOrderManagementModel()
		{
			//AllProducts = new List<Product>();
			//AllShipInfo = new List<ShippingInfo>();

			ProductIdList = new List<int>();
			QuantityList = new List<int>();
			PriceList = new List<int>();
			DiscountList = new List<double>();

			Order = new Order();
		}
	}

	public class EditOrderManagementModel
	{
		public Order Order { get; set; }
		public List<OrderDetail> Details { get; set; }
		public List<string?> Thumbnails { get; set; }

		public EditOrderManagementModel()
		{
			Order = new Order();
			Details = new List<OrderDetail>();
			Thumbnails = new List<string?>();
		}
	}

	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderRepository orderRepository;
		private readonly IShippingInfoRepository shippingInfoRepository;
		private readonly IProductRespository productRespository;
		private readonly IMapper mapper;
		private readonly IApplicationUserRespository userRepository;
		private readonly IImageRepository imageRepository;

		public OrderController(IOrderRepository orderRepository, IShippingInfoRepository shippingInfoRepository,
			IProductRespository productRespository, IMapper mapper, IApplicationUserRespository userRepository,
			IImageRepository imageRepository)
		{
			this.orderRepository = orderRepository;
			this.shippingInfoRepository = shippingInfoRepository;
			this.productRespository = productRespository;
			this.mapper = mapper;
			this.userRepository = userRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index()
		{
			var model = new ViewOrderManagementModel();
			
			model.Orders = await orderRepository.GetAllOrdersFullInfoAsync();
			return View(model);
		}

		public async Task<IActionResult> Add()
		{
			if (await GetCurrentLoggedInStaffAsync() == null)
				return Unauthorized();
			
			//var shipInfoTask = shippingInfoRepository.GetAllShipInfoAsync();
			//var productTask = productRespository.GetAllProductsAsync();
			
			var model = new AddOrderManagementModel();
			//model.AllShipInfo = await shipInfoTask;
			//model.AllProducts = await productTask;
			
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddOrderManagementModel model)
		{
			var staff = await GetCurrentLoggedInStaffAsync();
			if (staff == null)
				return Unauthorized();
			
			model.Order.OrderDate = DateTime.Now;
			model.Order.StaffId = staff.StaffId;

			double totalPrice = 0;
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

				totalPrice += detail.UnitPrice.Value * (1 - detail.Discount.Value / 100) * detail.Quantity.Value;

				detailList.Add(detail);
			}

			totalPrice *= 1 + model.Order.VAT.Value / 100;

			if (model.Order.AmountPaid.Equals((int)totalPrice))
				model.Order.Status = OrderStatus.Packaging;
			else model.Order.Status = OrderStatus.Payment;

			await orderRepository.AddOrderAsync(model.Order, detailList);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Detail(int id)
		{
			var model = new EditOrderManagementModel();

			var order = await orderRepository.GetOrderByIdFullInfoAsync(id);
			if (order == null)
				return NotFound();
			
			var detailsTask = orderRepository.GetOrderDetailByIdFullInfoAsync(id);

			var thumbnails = new List<string?>();
			foreach (var o in order.OrderDetails)
			{
				var image = await imageRepository.GetThumbnailByProductIdAsync(o.ProductId);
				if (image != null)
				{
					thumbnails.Add(image.ImageName);
				}
				else
				{
					thumbnails.Add(null);
				}
			}

			var details = await detailsTask;

			model.Order = order;
			model.Details = details;
			model.Thumbnails = thumbnails;

			return View(model);
		}

		//---------- Other functions ----------

		private async Task<Staff?> GetCurrentLoggedInStaffAsync()
		{
			try
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
				//get application user by id
				var appUser = await userRepository.GetStaffByUIdAsync(userId);//lấy ra các thông tin liên quan đến user bằng userID(Application là bảng User)
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
		public async Task<ActionResult<List<ProductDTO>>> GetAllActiveProducts()
		{
			var products = await productRespository.GetAllProductsForOrderCreationAsync();

			return products.Select(mapper.Map<Product, ProductDTO>).ToList();
		}

		[HttpGet]
		public async Task<ActionResult<List<ShippingInfo>>> GetAllShippingInfo()
		{
			var shipInfos = await shippingInfoRepository.GetAllShipInfoAsync();

			return shipInfos;
		}

		[HttpPost]
		public async Task<IActionResult> AddNewShippingInfo([FromBody] ShippingInfo shipInfo)
		{
			ModelState.Remove("ProvinceCode");
			ModelState.Remove("DistrictCode");
			ModelState.Remove("WardCode");
            if (!ModelState.IsValid)
			{
				return StatusCode(404, "Lỗi xảy ra! Vui lòng thử lại sau.");
			}
			try
			{
				await shippingInfoRepository.AddShippingInfoAsync(shipInfo);
				return Ok(shipInfo);
			}
			catch (Exception ex)
			{
				//throw new Exception(ex.Message);
				return StatusCode(404, "Có lỗi xảy ra...");
			}
		}

		#endregion

	}

}
