using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.DataAccess.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{
	// Khong dat duoc qua so luong hang trong kho
	// Khi tao shipping info moi luc check out -> Neu chua co shipping info nao -> De default luon
	//		(can test lai)
	
	// Cart khi chua login chi chua duoc ~20-30 item (gioi han size cookie)
	// Cho khach hang them ghi chu khi dat hang?

	public class OrderPurchaseModel
	{
		public List<CartDetail> Cart { get; set; }
		public ShippingInfo Address { get; set; }
		public int AddressId { get; set; }

		public OrderPurchaseModel()
		{
			Cart = new List<CartDetail>();
			Address = new ShippingInfo();
			AddressId = 0;
		}
	}

	public class AddAddressModel
	{
		public string Method { get; set; }
		public ShippingInfo ShipInfo { get; set; }
		public int CurrentShipInfoId { get; set; }

		public AddAddressModel()
		{
			Method = "";
			ShipInfo = new ShippingInfo();
			CurrentShipInfoId = 0;
		}
	}
	
	[Area("Customer")]
	public class OrderController : Controller
	{
		private readonly IShippingInfoRepository shippingInfoRepository;
		private readonly IOrderRepository orderRepository;

		private readonly OrderFunctions functions;

		private readonly ICustomerRespository customerRespository;

		public OrderController(IProductRespository productRespository, 
			IApplicationUserRespository userRepository, ICartRepository cartRepository,
			IHttpContextAccessor httpContextAccessor, IShippingInfoRepository shippingInfoRepository,
			IOrderRepository orderRepository, IImageRepository imageRepository, ICustomerRespository customerRespository)
		{
			functions = new OrderFunctions(productRespository, userRepository, cartRepository, httpContextAccessor, imageRepository);
			this.shippingInfoRepository = shippingInfoRepository;
			this.orderRepository = orderRepository;
			this.customerRespository = customerRespository;
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int number, int id)
		{
			bool success = await functions.AddToCartFunctionAsync(number, id);

			if (!success)
				return NotFound();
			
			return RedirectToAction("Detail", "Product", new { id = id });
		}

		[HttpPost]
		public async Task<IActionResult> BuyNow(int number, int id)
		{
			bool success = await functions.AddToCartFunctionAsync(number, id);

			if (!success)
				return NotFound();

			return RedirectToAction("Cart");
		}

		public async Task<IActionResult> Cart()
		{
			var customerId = await functions.GetLoginCustomerIdAsync();
			
			var cartTask = functions.GetCartAsync(customerId);

			var thumbnails = await functions.GetThumbnailsAsync();
			ViewData["thumbnails"] = thumbnails;

			var cart = await cartTask;
			return View(cart);
		}

		public async Task<IActionResult> PlusOneToCart(int id)
		{
			bool success = await functions.AddToCartFunctionAsync(1, id);

			if (!success)
				return NotFound();

			return RedirectToAction("Cart");
		}

		public async Task<IActionResult> MinusOneFromCart(int id)
		{
			bool success = await functions.AddToCartFunctionAsync(-1, id);

			if (!success)
				return NotFound();

			return RedirectToAction("Cart");
		}

		public async Task<IActionResult> RemoveItemFromCart(int id)
		{
			var customerId = await functions.GetLoginCustomerIdAsync();
			
			var cart = await functions.GetCartAsync(customerId);
			var detail = cart.SingleOrDefault(d => d.ProductId == id);
			if (detail == null)
				return NotFound();

			await functions.AddToCartFunctionAsync(-(int)detail.Quantity, id);
			return RedirectToAction("Cart");

		}

		public async Task<IActionResult> Purchase()
		{
			var customer = await functions.GetLoginCustomerAsync();
			if (customer == null)
				return Unauthorized();

			var model = new OrderPurchaseModel();
			model.Cart = await functions.GetCartAsync(customer.CustomerId);
			if (model.Cart.Count <= 0)
				return BadRequest();

			model.AddressId = 0;
			if (customer.DefaultShippingInfoId != null)
			{
				model.AddressId = customer.DefaultShippingInfoId.Value;
				model.Address = await shippingInfoRepository.GetShippingInfoByIdAsync(model.AddressId);
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Purchase(int shipInfoId)
		{
			var model = new OrderPurchaseModel();
			model.Cart = await functions.GetCartAsync(await functions.GetLoginCustomerIdAsync());
			if (model.Cart.Count <= 0)
				return BadRequest();

			if (shipInfoId != 0)
			{
				var shipInfo = await shippingInfoRepository.GetShippingInfoByIdAsync(shipInfoId);
				if (shipInfo == null)
					return NotFound();
				model.Address = shipInfo;
				model.AddressId = shipInfoId;
			}
			
			return View("Purchase", model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangeAddress(int shipInfoId)
		{
			var custId = await functions.GetLoginCustomerIdAsync();
			if (custId == null)
				return Unauthorized();

			var shipInfoList = await shippingInfoRepository.GetAllShippingInfosByCustomerIdAsync((int)custId);

			ViewData["selectedAddress"] = shipInfoId;

			return View("ChangeAddress", shipInfoList);
		}

		[HttpPost]
		public async Task<IActionResult> AddAddress(AddAddressModel model)
		{
			var cust = await functions.GetLoginCustomerAsync();
			if (cust == null)
				return Unauthorized();
			var custId = cust.CustomerId;

			if (model.Method.Equals("get"))
			{
				return View(model);
			}
			else if (model.Method.Equals("post"))
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}
				
				model.ShipInfo.CustomerId = custId;
				model.ShipInfo.Status = true;

				int latestShipInfoId = await shippingInfoRepository.AddShippingInfoAsync(model.ShipInfo);
				if (cust.DefaultShippingInfoId == null)
					await customerRespository.UpdateCustomerDefaultShipInfoAsync(custId, latestShipInfoId);
				
				// Khong phai cach lam tot nhat, nhung tam the vay :v
				return await ChangeAddress(latestShipInfoId);
			}
			else return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> PurchaseFinalize(int shipInfoId)
		{
			// Neu front end thuan loi thi khong can cai nay, nhung cu them vao cho an toan :v
			if (shipInfoId == 0)
				return await Purchase(shipInfoId);

			var order = new Order();
			var details = new List<OrderDetail>();

			var shipInfo = await shippingInfoRepository.GetShippingInfoByIdAsync(shipInfoId);
			if (shipInfo == null)
				return BadRequest();

			var custId = await functions.GetLoginCustomerIdAsync();
			if (custId == null)
				return Unauthorized();

			var cart = await functions.GetCartAsync(custId);
			if (cart.Count <= 0)
				return BadRequest();

			order.OrderDate = DateTime.Now;

			//order.ShippingInfo = shipInfo.CustomerName + ", " + shipInfo.Phone + ", " + shipInfo.ShipAddress;

			order.CustomerName = shipInfo.CustomerName;
			order.Phone = shipInfo.Phone;
			order.ShipAddress = shipInfo.ShipAddress;

			order.Status = OrderStatus.Wait;
			order.CustomerId = custId;

			order.AmountPaid = 0;
			order.VAT = 0;

			foreach (var item in cart)
			{
				var detail = new OrderDetail();
				detail.ProductId = item.ProductId;
				detail.Quantity = item.Quantity;
				detail.UnitPrice = item.Product.RetailPrice;
				detail.Discount = item.Product.RetailDiscount;

				details.Add(detail);
			}

			var addOrderTask = orderRepository.AddOrderAsync(order, details);
			var clearCartTask = functions.ClearCartAsync();

			Task.WaitAll(addOrderTask, clearCartTask);

			return RedirectToAction("Index", "Home");
		}

	}

	public class OrderFunctions
	{
		HttpRequest Request;
		HttpResponse Response;
		ClaimsPrincipal User;

		private readonly IProductRespository productRespository;
		private readonly IApplicationUserRespository userRepository;
		private readonly ICartRepository cartRepository;
		private readonly IImageRepository imageRepository;

		public OrderFunctions(IProductRespository productRespository, 
			IApplicationUserRespository userRepository, ICartRepository cartRepository, 
			IHttpContextAccessor httpContextAccessor, IImageRepository imageRepository)
		{
			this.productRespository = productRespository;
			this.userRepository = userRepository;
			this.cartRepository = cartRepository;

			Request = httpContextAccessor.HttpContext.Request;
			Response = httpContextAccessor.HttpContext.Response;
			User = httpContextAccessor.HttpContext.User;

			this.imageRepository = imageRepository;
		}

		public async Task<Models.Customer?> GetLoginCustomerAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) == null)
				return null;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			var user = await userRepository.GetCustomerByUIdAsync(userId);
			if (user == null) return null;

			var customer = user.Customer;
			if (customer == null) return null;

			return customer;
		}

		public async Task<int?> GetLoginCustomerIdAsync()
		{
			var customer = await GetLoginCustomerAsync();
			
			if (customer == null)
				return null;

			return customer.CustomerId;
		}

		public async Task<List<CartDetail>> GetCartAsync(int? customerId)
		{
			if (customerId == null)
			{
				if (Request.Cookies["Cart"] == null)
				{
					return new List<CartDetail>();
				}
				else
				{
					string cartJson = Request.Cookies["Cart"];
					List<CartDetail> cart = JsonSerializer.Deserialize<List<CartDetail>>(cartJson);
					foreach (var detail in cart)
					{
						var product = await productRespository.GetProductByIdAsync(detail.ProductId);
						detail.Product = product;
					}
					return cart;
				}
			}
			else
			{
				return await cartRepository.GetCartByCustomerIdAsync(customerId.Value);
			}
		}

		public async Task<Dictionary<int, string?>> GetThumbnailsAsync()
		{
			Dictionary<int, string?> thumbnails = new Dictionary<int, string?>();
			var allProducts = await productRespository.GetAllProductsAsync();
			foreach (var product in allProducts)
			{
				int id = product.ProductId;
				Image? thumbnail = await imageRepository.GetThumbnailByProductIdAsync(id);
				string? thumbnailName;
				if (thumbnail == null)
					thumbnailName = null;
				else thumbnailName = thumbnail.ImageName;
				thumbnails.Add(id, thumbnailName);
			}

			return thumbnails;
		}

		public async Task<bool> AddToCartFunctionAsync(int number, int id)
		{
			var customerId = await GetLoginCustomerIdAsync();
			
			if (customerId == null)
			{
				var cart = await GetCartAsync(customerId);
				var detail = cart.SingleOrDefault(c => c.ProductId == id);

				if (detail != null)
				{
					detail.Quantity += number;

					if (detail.Quantity <= 0)
					{
						// Remove item from cart in cookie
						cart.Remove(detail);
					}
				}
				else
				{
					cart.Add(new CartDetail
					{
						Quantity = number,
						ProductId = id
					});
				}
				
				AddCartToCookies(cart);
			}
			else
			{
				var detail = await cartRepository.GetCartItemAsync(customerId.Value, id);
				if (detail != null)
				{
					detail.Quantity += number;
					
					if (detail.Quantity <= 0)
					{
						// Remove item from cart in DB
						await cartRepository.DeleteItemFromCartAsync(customerId.Value, id);
					}
					else
					{
						// Update cart in DB
						await cartRepository.UpdateCartAsync(detail);
					}
				}
				else
				{
					if (number < 0)
						return false;

					// Add to cart in DB
					await cartRepository.AddItemToCartAsync(new CartDetail
					{
						ProductId = id,
						CustomerId = customerId.Value,
						Quantity = number
					});
				}
			}

			return true;
		}

		void AddCartToCookies(List<CartDetail> cart)
		{
			foreach (var item in cart)
			{
				item.Product = null;
			}

			string cartJson = JsonSerializer.Serialize(cart);
			var options = new CookieOptions();

			// Expires after 1 month if not login
			options.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Append("Cart", cartJson, options);
		}

		public async Task ClearCartAsync()
		{
			var custId = await GetLoginCustomerIdAsync();
			if (custId != null)
			{
				await cartRepository.ClearCartByCustomerIdAsync((int)custId);
			}
			Response.Cookies.Delete("Cart");
		}

		public async Task AddCartAtLoginAsync()
		{
			var customerId = await GetLoginCustomerIdAsync();
			if (customerId == null) return;

			// Get current cart in cookie;
			var cookieCart = await GetCartAsync(null);

			// Get current customer cart in DB
			var dbCart = await cartRepository.GetCartByCustomerIdAsync(customerId.Value);

			// Add cookie cart to DB cart
			foreach (var item in cookieCart)
			{
				var detail = dbCart.SingleOrDefault(c => c.ProductId == item.ProductId);
				if (detail == null)
				{
					item.CustomerId = customerId.Value;
					item.Product = null;
					await cartRepository.AddItemToCartAsync(item);
				}
				else
				{
					detail.Quantity += item.Quantity;
					await cartRepository.UpdateCartAsync(detail);
				}
			}

			// Remove cart in cookie
			Response.Cookies.Delete("Cart");
		}
	}
}
