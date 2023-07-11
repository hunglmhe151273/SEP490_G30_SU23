using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.DataAccess.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{		
	// Chua co man hinh them dia chi
	// Chua co man hinh chon dia chi
	
	// Dung Observer pattern cho AddCartAtLogin, RemoveCartAtLogout -> HOW?
	// Khong dat duoc qua so luong hang trong kho
	// Neu khach ko Remember me -> RemoveCartAtLogout luon khi tat browser

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
	
	[Area("Customer")]
	public class OrderController : Controller
	{
		//private readonly IProductRespository productRespository;
		//private readonly IApplicationUserRespository userRepository;
		//private readonly ICartRepository cartRepository;
		private readonly IShippingInfoRepository shippingInfoRepository;

		private readonly OrderFunctions functions;

		public OrderController(IProductRespository productRespository, 
			IApplicationUserRespository userRepository, ICartRepository cartRepository,
			IHttpContextAccessor httpContextAccessor, IShippingInfoRepository shippingInfoRepository)
		{
			//this.productRespository = productRespository;
			//this.userRepository = userRepository;
			//this.cartRepository = cartRepository;

			functions = new OrderFunctions(productRespository, userRepository, cartRepository, httpContextAccessor);
			this.shippingInfoRepository = shippingInfoRepository;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart(int number, int id)
		{
			//bool success = await AddToCartFunctionAsync(number, id);
			bool success = await functions.AddToCartFunctionAsync(number, id);

			if (!success)
				return NotFound();
			
			return RedirectToAction("Detail", "Product", new { id = id });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BuyNow(int number, int id)
		{
			//bool success = await AddToCartFunctionAsync(number, id);
			bool success = await functions.AddToCartFunctionAsync(number, id);

			if (!success)
				return NotFound();

			return RedirectToAction("Cart");
		}

		public IActionResult Cart()
		{
			//var cart = GetCartFromCookies();
			var cart = functions.GetCartFromCookies();
			
			return View(cart);
		}

		public async Task<IActionResult> PlusOneToCart(int id)
		{
			//bool success = await AddToCartFunctionAsync(1, id);
			bool success = await functions.AddToCartFunctionAsync(1, id);

			if (!success)
				return NotFound();

			return RedirectToAction("Cart");
		}

		public async Task<IActionResult> MinusOneFromCart(int id)
		{
			//bool success = await AddToCartFunctionAsync(-1, id);
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
			var customerId = await functions.GetLoginCustomerIdAsync();
			if (customerId == null)
				return Unauthorized();

			var model = new OrderPurchaseModel();
			model.Cart = functions.GetCartFromCookies();
			if (model.Cart.Count <= 0)
				return BadRequest();

			model.AddressId = 0;
			var shipInfoList = await shippingInfoRepository.GetAllShippingInfosByCustomerIdAsync((int)customerId);
			if (shipInfoList.Count > 0)
			{
				model.Address = shipInfoList[0];
				model.AddressId = shipInfoList[0].ShipInfoId;
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Purchase(int shipInfoId)
		{
			var model = new OrderPurchaseModel();
			model.Cart = functions.GetCartFromCookies();
			if (model.Cart.Count <= 0)
				return BadRequest();

			var shipInfo = await shippingInfoRepository.GetShippingInfoByIdAsync(shipInfoId);
			if (shipInfo == null)
				return NotFound();
			model.Address = shipInfo;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PurchaseFinalize(int shipInfoId)
		{
			// TBD
			return RedirectToAction("Index", "Home");
		}

		//---------- Other functions ----------

		//async Task<int?> GetLoginCustomerIdAsync()
		//{
		//	var claimsIdentity = (ClaimsIdentity)User.Identity;
		//	if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) == null)
		//		return null;
		//	var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		//	var user = await userRepository.GetCustomerByUIdAsync(userId);
		//	if (user == null) return null;

		//	var customer = user.Customer;
		//	if (customer == null) return null;

		//	return customer.CustomerId;
		//}

		//async Task<bool> AddToCartFunctionAsync(int number, int id)
		//{
		//	var cart = GetCartFromCookies();
		//	var customerIdTask = GetLoginCustomerIdAsync();

		//	var detail = cart.SingleOrDefault(c => c.ProductId == id);
		//	if (detail != null)
		//	{
		//		var customerId = await customerIdTask;

		//		// Update cart in cookie
		//		detail.Quantity += number;

		//		if (detail.Quantity == 0)
		//		{
		//			// Remove item from cart in cookie
		//			cart.Remove(detail);

		//			// Remove item from cart in DB
		//			if (customerId != null)
		//			{
		//				await cartRepository.DeleteItemFromCartAsync((int)customerId, id);
		//			}
		//		}
		//		else
		//		{
		//			// Update cart in DB
		//			if (customerId != null)
		//			{
		//				await cartRepository.UpdateCartAsync(new CartDetail
		//				{
		//					CustomerId = (int)customerId,
		//					ProductId = id,
		//					Quantity = detail.Quantity
		//				});
		//			}
		//		}
		//	}
		//	else
		//	{
		//		if (number < 0)
		//			return false;

		//		var product = await productRespository.GetProductByIdAsync(id);
		//		if (product == null)
		//			return false;

		//		// Add to cart in cookie
		//		cart.Add(new CartDetail
		//		{
		//			ProductId = id,
		//			Quantity = number,

		//			Product = product
		//		});

		//		// Add to cart in DB
		//		var customerId = await customerIdTask;
		//		if (customerId != null)
		//		{
		//			await cartRepository.AddItemToCartAsync(new CartDetail
		//			{
		//				ProductId = id,
		//				CustomerId = (int)customerId,
		//				Quantity = number
		//			});
		//		}
		//	}

		//	AddCartToCookies(cart);
		//	return true;
		//}

		//List<CartDetail> GetCartFromCookies()
		//{
		//	if (Request.Cookies["Cart"] == null)
		//	{
		//		return new List<CartDetail>();
		//	}
		//	else
		//	{
		//		string cartJson = Request.Cookies["Cart"];
		//		List<CartDetail> cart = JsonSerializer.Deserialize<List<CartDetail>>(cartJson);
		//		return cart;
		//	}
		//}

		//void AddCartToCookies(List<CartDetail> cart)
		//{
		//	string cartJson = JsonSerializer.Serialize(cart);
		//	var options = new CookieOptions();
		//	if (GetLoginCustomerIdAsync().GetAwaiter().GetResult() != null)
		//	{
		//		// Never expires, only remove when logout
		//		options.Expires = DateTime.Now.AddYears(50);
		//	}
		//	else
		//	{
		//		// Expires after 1 month if not login
		//		options.Expires = DateTime.Now.AddMonths(1);
		//	}
		//	Response.Cookies.Append("Cart", cartJson, options);
		//}

		//public async Task AddCartAtLoginAsync()
		//{
		//	var customerId = await GetLoginCustomerIdAsync();
		//	if (customerId == null) return;

		//	// Get current cart in cookie
		//	var cart = GetCartFromCookies();
		//	var cartInCookie = new List<CartDetail>();
		//	foreach (var item in cart)
		//	{
		//		cartInCookie.Add(new CartDetail
		//		{
		//			ProductId = item.ProductId,
		//			CustomerId = (int)customerId,
		//			Quantity = item.Quantity
		//		});
		//	}

		//	// Get current cart in DB
		//	var customerCart = await cartRepository.GetCartByCustomerIdAsync((int)customerId);

		//	// Add cart from cookie to DB;
		//	var updateDbTask = cartRepository.AddOrUpdateMultipleItemsToCartAsync(cartInCookie);

		//	// Add cart from DB to cookie
		//	foreach (var cartItem in customerCart)
		//	{
		//		var detail = cart.SingleOrDefault(c => c.ProductId == cartItem.ProductId);
		//		if (detail != null)
		//		{
		//			detail.Quantity += cartItem.Quantity;
		//		}
		//		else
		//		{
		//			var product = await productRespository.GetProductByIdAsync(cartItem.ProductId);
		//			if (product == null) return;

		//			cart.Add(new CartDetail
		//			{
		//				ProductId = cartItem.ProductId,
		//				Quantity = cartItem.Quantity,

		//				Product = product
		//			});
		//		}
		//	}
		//	AddCartToCookies(cart);

		//	await updateDbTask;
		//}

		//public void RemoveCartAtLogout()
		//{
		//	Response.Cookies.Delete("Cart");
		//}
	}

	public class OrderFunctions
	{
		HttpRequest Request;
		HttpResponse Response;
		ClaimsPrincipal User;

		private readonly IProductRespository productRespository;
		private readonly IApplicationUserRespository userRepository;
		private readonly ICartRepository cartRepository;

		public OrderFunctions(IProductRespository productRespository, 
			IApplicationUserRespository userRepository, ICartRepository cartRepository, 
			IHttpContextAccessor httpContextAccessor)
		{
			this.productRespository = productRespository;
			this.userRepository = userRepository;
			this.cartRepository = cartRepository;

			Request = httpContextAccessor.HttpContext.Request;
			Response = httpContextAccessor.HttpContext.Response;
			User = httpContextAccessor.HttpContext.User;
		}

		public async Task<int?> GetLoginCustomerIdAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) == null)
				return null;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			var user = await userRepository.GetCustomerByUIdAsync(userId);
			if (user == null) return null;

			var customer = user.Customer;
			if (customer == null) return null;

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
