﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Utility;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;

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
		public List<Staff> Staffs { get; set; }

		public ViewOrderManagementModel()
		{
			Orders = new List<Order>();
			Staffs = new List<Staff>();
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
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
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
			model.Staffs = await orderRepository.GetAllStaffAsync();
			return View(model);
		}

		//[Authorize(Roles = SD.Role_Staff)]
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

		public async Task<IActionResult> ExportInvoice(int id)
		{
			var body = await PdfBody(id);
			if (body != null)
			{
				var document = new PdfDocument();
				PdfGenerator.AddPdfPages(document, body, PageSize.Letter);
				byte[]? response = null;
				using (MemoryStream stream = new MemoryStream())
				{
					document.Save(stream);
					response = stream.ToArray();
				}
				string fileName = "Invoice_" + id + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
				return File(response, "application/pdf", fileName);
			}
			else
			{
				return NotFound();
			}
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

		private async Task<string?> PdfBody(int orderId)
		{
			var order = await orderRepository.GetOrderByIdFullInfoAsync(orderId);
			if (order == null)
				return null;
			var details = await orderRepository.GetOrderDetailByIdFullInfoAsync(orderId);

			string staffName = "";
			if (order.Staff != null)
				staffName = order.Staff.FullName;
			string firstPart = $@"
				<style>
					table.main{{
						width: 100%;
					}}
					table.main, table.main th, table.main td{{
						border: 1px solid;
						border-collapse: collapse;
						text-align: center;
					}}
					table.end{{
						width: 100%;
						text-align: right;
						margin-top: 20px;
					}}
					table.end td{{
						width: 15%;
					}}
					table.end th, table.end td{{
						font-weight: bold;
						font-size: 1.17em;
						text-align: right;
					}}
					table.start{{
						width: 100%;
						text-align: left;
						margin-bottom: 20px;
					}}
					table.start th{{
						width: 15%;
					}}
					table.start th, table.start td{{
						text-align: left;
					}}
				</style>
				
				<h1 style=""text-align: center"">HÓA ĐƠN BÁN HÀNG</h1>
				<p style=""text-align: center"">{order.OrderDate.Value.ToString("dd/MM/yyyy")}</p>
				<table class=""start"">
					<tr>
						<th>Mã hóa đơn: </th>
						<td>#{order.OrderId}</td>
					</tr>
					<tr>
						<th>Nhân viên: </th>
						<td>{staffName}</td>
					</tr>
					<tr>
						<th>Khách hàng: </th>
						<td>{order.Customer.FullName}</td>
					</tr>
				</table>
				<table class=""main"">
					<tr>
						<th style=""width: 10%"">STT</th>
						<th style=""width: 40%"">Hàng hóa</th>
						<th style=""width: 10%"">SL</th>
						<th style=""width: 15%"">Đ.Giá</th>
						<th style=""width: 10%"">KM</th>
						<th style=""width: 15%"">T.Tiền</th>
					</tr>						
				";

			double totalAfter = 0;
			double totalDiscount = 0;
			StringBuilder sb = new StringBuilder();
			int stt = 0;
			foreach (var item in details)
			{
				++stt;
				var itemPrice = item.Quantity.Value * item.UnitPrice.Value * (1 - item.Discount.Value / 100);

				sb.Append($@"
					<tr>
						<td>{stt}</td>
						<td>{item.Product.Name}</td>
						<td>{item.Quantity.Value.ToString("#,0")}</td>
						<td>{item.UnitPrice.Value.ToString("#,0")}</td>
						<td>{item.Discount}</td>
						<td>{itemPrice.ToString("#,0")}</td>
					</tr>
					");
				totalDiscount += item.UnitPrice.Value * (item.Discount.Value / 100);
				totalAfter += itemPrice;
			}
			string middlePart = sb.ToString();

			string lastPart = $@"
				</table>
				<table class=""end"">
					<tr>
						<th>Tổng tiền:</th>
						<td>{(totalAfter + totalDiscount).ToString("#,0")}</td>
					</tr>
					<tr>
						<th>Tổng KM:</th>
						<td>{totalDiscount.ToString("#,0")}</td>
					</tr>
					<tr>
						<th>Thanh toán:</th>
						<td>{totalAfter.ToString("#,0")}</td>
					</tr>
					<tr>
						<th>Khách trả:</th>
						<td>{order.AmountPaid.Value.ToString("#,0")}</td>
					</tr>
					<tr>
						<th>Còn lại:</th>
						<td>{(totalAfter - order.AmountPaid.Value).ToString("#,0")}</td>
					</tr>
				</table>
				";

			return firstPart + middlePart + lastPart;
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
