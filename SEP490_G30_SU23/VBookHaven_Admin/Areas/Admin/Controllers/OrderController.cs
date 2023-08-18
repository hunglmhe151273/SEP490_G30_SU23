using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Utility;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
	// Chua chon duoc tinh thanh khi them dia chi giao hang
	// Gui email khi update trang thai don hang
	// Them trang thai Xuat kho?
	// Index: Ten nguoi nhan hang -> Ten khach hang
	// Index: Default don moi nhat len dau

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

		public string PaymentMethod { get; set; }


		public AddOrderManagementModel()
		{
			//AllProducts = new List<Product>();
			//AllShipInfo = new List<ShippingInfo>();

			ProductIdList = new List<int>();
			QuantityList = new List<int>();
			PriceList = new List<int>();
			DiscountList = new List<double>();

			Order = new Order();

			PaymentMethod = "";
		}
	}

	public class EditOrderManagementModel
	{
		public Order Order { get; set; }
		public List<OrderDetail> Details { get; set; }
		public List<string?> Thumbnails { get; set; }
		public List<OrderPaymentHistory> Payments { get; set; }

		public EditOrderManagementModel()
		{
			Order = new Order();
			Details = new List<OrderDetail>();
			Thumbnails = new List<string?>();
            Payments = new List<OrderPaymentHistory>();
        }
    }

	[Area("Admin")]
	[Authorize]
    public class OrderController : Controller
	{
		private readonly IOrderRepository orderRepository;
		private readonly IShippingInfoRepository shippingInfoRepository;
		private readonly IProductRespository productRespository;
		private readonly IMapper mapper;
		private readonly IApplicationUserRespository userRepository;
		private readonly IImageRepository imageRepository;
		private readonly ICustomerRespository customerRespository;

		public OrderController(IOrderRepository orderRepository, IShippingInfoRepository shippingInfoRepository,
			IProductRespository productRespository, IMapper mapper, IApplicationUserRespository userRepository,
			IImageRepository imageRepository, ICustomerRespository customerRespository)
		{
			this.orderRepository = orderRepository;
			this.shippingInfoRepository = shippingInfoRepository;
			this.productRespository = productRespository;
			this.mapper = mapper;
			this.userRepository = userRepository;
			this.imageRepository = imageRepository;
			this.customerRespository = customerRespository;
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
                return RedirectToAction("Login", "Account", new { area = "Identity" });

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

			model.Order.Status = OrderStatus.Process;

			await orderRepository.AddOrderAsync(model.Order, detailList);

			if (model.Order.AmountPaid > 0)
			{
				var payment = new OrderPaymentHistory()
				{
					PaymentDate = model.Order.OrderDate,
					PaymentAmount = model.Order.AmountPaid,
					PaymentMethod = model.PaymentMethod,
					OrderId = model.Order.OrderId,
					StaffId = staff.StaffId,
				};

				await orderRepository.AddOrderPaymentHistoryAsync(payment);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Detail(int id)
		{
			var model = new EditOrderManagementModel();

			var order = await orderRepository.GetOrderByIdFullInfoAsync(id);
			if (order == null)
				return NotFound();
			
			var detailsTask = orderRepository.GetOrderDetailByIdFullInfoAsync(id);
			var paymentTask = orderRepository.GetOrderPaymentHistoryByIdFullInfoAsync(id);

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
			var payments = await paymentTask;

			model.Order = order;
			model.Details = details;
			model.Thumbnails = thumbnails;
			model.Payments = payments;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int id)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(id);
			if (order == null)
				return NotFound();

			var staff = await GetCurrentLoggedInStaffAsync();
			if (staff == null)
				return Unauthorized();

			switch (order.Status)
			{
				case (OrderStatus.Wait):
					order.Status = OrderStatus.Process;
					order.StaffId = staff.StaffId;
					await orderRepository.UpdateOrderAsync(order);
					break;
				case (OrderStatus.Process):
					var exportTask = ExportFromStorage(id);
					order.Status = OrderStatus.Shipping;
					await orderRepository.UpdateOrderAsync(order);
					await exportTask;
					break;
				case (OrderStatus.Shipping):
					var total = await orderRepository.GetOrderTotalCostAsync(id);
					if (order.AmountPaid == (int)total)
					{
						order.Status = OrderStatus.Done;
					}
					else order.Status = OrderStatus.Shipped;
					await orderRepository.UpdateOrderAsync(order);
					break;
			}

			return RedirectToAction("Detail", new { id = id }); 
		}

		[HttpPost]
		public async Task<IActionResult> Cancel(int id)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(id);
			if (order == null)
				return NotFound();

			var staff = await GetCurrentLoggedInStaffAsync();
			if (staff == null)
				return Unauthorized();

			foreach (var detail in order.OrderDetails)
			{
				var product = await productRespository.GetProductByIdAsync(detail.ProductId);
				product.AvailableUnit += detail.Quantity;
				await productRespository.UpdateProductAsync(product);
			}

			if (order.Status.Equals(OrderStatus.Wait))
			{
				order.StaffId = staff.StaffId;
				order.Status = OrderStatus.Cancel;
				await orderRepository.UpdateOrderAsync(order);
			}
			else if (order.Status.Equals(OrderStatus.Shipping))
			{
				var undoTask = UndoExportFromStorage(id);
				order.Status = OrderStatus.Cancel;
				await orderRepository.UpdateOrderAsync(order);
				await undoTask;
			}
			else
			{
				order.Status = OrderStatus.Cancel;
				await orderRepository.UpdateOrderAsync(order);
			}

			return RedirectToAction("Detail", new { id = id });
		}

		[HttpPost]
		public async Task<IActionResult> AddPayment(int id, string method, DateTime date, int amount)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(id);
			if (order == null)
				return NotFound();

			var staff = await GetCurrentLoggedInStaffAsync();
			if (staff == null)
				return Unauthorized();

			var totalTask = orderRepository.GetOrderTotalCostAsync(id);

			var payment = new OrderPaymentHistory()
			{
				PaymentDate = date,
				PaymentAmount = amount,
				PaymentMethod = method,
				OrderId = id,
				StaffId = staff.StaffId
			};

			var paymentTask = orderRepository.AddOrderPaymentHistoryAsync(payment);

			order.AmountPaid += amount;
			var total = await totalTask;
			if (order.AmountPaid.Equals((int)total) && order.Status.Equals(OrderStatus.Shipped))
			{
				order.Status = OrderStatus.Done;
			}
			var orderTask = orderRepository.UpdateOrderAsync(order);

			Task.WaitAll(paymentTask, orderTask);
			return RedirectToAction("Detail", new { id = id });
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

		private async Task ExportFromStorage(int orderId)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(orderId);
			if (order == null)
				return;

			foreach (var detail in order.OrderDetails)
			{
				var product = await productRespository.GetProductByIdAsync(detail.ProductId);
				product.UnitInStock -= detail.Quantity;
				await productRespository.UpdateProductAsync(product);
			}	
		}

		private async Task UndoExportFromStorage(int orderId)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(orderId);
			if (order == null)
				return;

			foreach (var detail in order.OrderDetails)
			{
				var product = await productRespository.GetProductByIdAsync(detail.ProductId);
				product.UnitInStock += detail.Quantity;
				await productRespository.UpdateProductAsync(product);
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
		public async Task<ActionResult<List<ShippingInfo>>> GetAllShipInfos()
		{
			var shipInfos = await customerRespository.GetAllNoAccountShippingInfoAsync();
			return shipInfos;
		}

		[HttpGet]
		public async Task<ActionResult<List<VBookHaven.Models.Customer>>> GetAllCustomers()
		{
			var customers = await customerRespository.GetAllNoAccountCustomersAsync();
			return customers;
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
				if (shipInfo.Customer != null)
				{
					await customerRespository.UpdateCustomerDefaultShipInfoAsync(shipInfo.Customer.CustomerId, shipInfo.ShipInfoId);
				}	
				
				if (shipInfo.Customer == null)
				{
					shipInfo.Customer = await customerRespository.GetCustomerByIdAsync(shipInfo.CustomerId.Value);
				}	
				
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
