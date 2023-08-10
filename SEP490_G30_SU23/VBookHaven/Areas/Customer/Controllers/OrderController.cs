using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.DataAccess.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{
	// Neu ko co default shipping info -> Chon dia chi dau tien?
	// Khong dat duoc qua so luong hang trong kho
	// Neu khach ko Remember me -> RemoveCartAtLogout luon khi tat browser

	// Khi tao shipping info moi luc check out -> Neu chua co shipping info nao -> De default luon

	// Dung Observer pattern cho AddCartAtLogin, RemoveCartAtLogout? -> HOW???
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

		public OrderController(IProductRespository productRespository, 
			IApplicationUserRespository userRepository, ICartRepository cartRepository,
			IHttpContextAccessor httpContextAccessor, IShippingInfoRepository shippingInfoRepository,
			IOrderRepository orderRepository, IImageRepository imageRepository)
		{
			functions = new OrderFunctions(productRespository, userRepository, cartRepository, httpContextAccessor, imageRepository);
			this.shippingInfoRepository = shippingInfoRepository;
			this.orderRepository = orderRepository;
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
			var cart = functions.GetCartFromCookies();

			var thumbnails = await functions.GetThumbnailsAsync();
			ViewData["thumbnails"] = thumbnails;
			
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
			var cart = functions.GetCartFromCookies();
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
			model.Cart = functions.GetCartFromCookies();
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
			model.Cart = functions.GetCartFromCookies();
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
			var custId = await functions.GetLoginCustomerIdAsync();
			if (custId == null)
				return Unauthorized();

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

			var cart = functions.GetCartFromCookies();
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

		public async Task<bool> AddToCartFunctionAsync(int number, int id)
		{
			var cart = GetCartFromCookies();
			var customerIdTask = GetLoginCustomerIdAsync();

			var detail = cart.SingleOrDefault(c => c.ProductId == id);
			if (detail != null)
			{
				var customerId = await customerIdTask;

				// Update cart in cookie
				detail.Quantity += number;

				if (detail.Quantity == 0)
				{
					// Remove item from cart in cookie
					cart.Remove(detail);

					// Remove item from cart in DB
					if (customerId != null)
					{
						await cartRepository.DeleteItemFromCartAsync((int)customerId, id);
					}
				}
				else
				{
					// Update cart in DB
					if (customerId != null)
					{
						await cartRepository.UpdateCartAsync(new CartDetail
						{
							CustomerId = (int)customerId,
							ProductId = id,
							Quantity = detail.Quantity
						});
					}
				}
			}
			else
			{
				if (number < 0)
					return false;

				var product = await productRespository.GetProductByIdAsync(id);
				if (product == null)
					return false;

				// Add to cart in cookie
				cart.Add(new CartDetail
				{
					ProductId = id,
					Quantity = number,

					Product = product
				});

				// Add to cart in DB
				var customerId = await customerIdTask;
				if (customerId != null)
				{
					await cartRepository.AddItemToCartAsync(new CartDetail
					{
						ProductId = id,
						CustomerId = (int)customerId,
						Quantity = number
					});
				}
			}

			AddCartToCookies(cart);
			return true;
		}

		public List<CartDetail> GetCartFromCookies()
		{
			if (Request.Cookies["Cart"] == null)
			{
				return new List<CartDetail>();
			}
			else
			{
				string cartJson = Request.Cookies["Cart"];
				List<CartDetail> cart = JsonSerializer.Deserialize<List<CartDetail>>(cartJson);
				return cart;
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

		void AddCartToCookies(List<CartDetail> cart)
		{
			string cartJson = JsonSerializer.Serialize(cart);
			var options = new CookieOptions();
			if (GetLoginCustomerIdAsync().GetAwaiter().GetResult() != null)
			{
				// Never expires, only remove when logout
				options.Expires = DateTime.Now.AddYears(50);
			}
			else
			{
				// Expires after 1 month if not login
				options.Expires = DateTime.Now.AddMonths(1);
			}
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

			// Get current cart in cookie
			var cart = GetCartFromCookies();
			var cartInCookie = new List<CartDetail>();
			foreach (var item in cart)
			{
				cartInCookie.Add(new CartDetail
				{
					ProductId = item.ProductId,
					CustomerId = (int)customerId,
					Quantity = item.Quantity
				});
			}

			// Get current cart in DB
			var customerCart = await cartRepository.GetCartByCustomerIdAsync((int)customerId);

			// Add cart from cookie to DB;
			var updateDbTask = cartRepository.AddCartFromCookieToDbAsync(cartInCookie);

			// Add cart from DB to cookie
			foreach (var cartItem in customerCart)
			{
				var detail = cart.SingleOrDefault(c => c.ProductId == cartItem.ProductId);
				if (detail != null)
				{
					detail.Quantity += cartItem.Quantity;
				}
				else
				{
					var product = await productRespository.GetProductByIdAsync(cartItem.ProductId);
					if (product == null) return;

					cart.Add(new CartDetail
					{
						ProductId = cartItem.ProductId,
						Quantity = cartItem.Quantity,

						Product = product
					});
				}
			}
			AddCartToCookies(cart);

			await updateDbTask;
		}

		public void RemoveCartAtLogout()
		{
			Response.Cookies.Delete("Cart");
		}
	}
}
