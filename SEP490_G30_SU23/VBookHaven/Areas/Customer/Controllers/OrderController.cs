using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VBookHaven.Models;
using VBookHaven.Respository;

namespace VBookHaven.Areas.Customer.Controllers
{
	// Them bang cart cho registered customer trong db? (De luu lai sau khi log out)
	
	[Area("Customer")]
	public class OrderController : Controller
	{
		private readonly IProductRespository productRespository;

		public OrderController(IProductRespository productRespository)
		{
			this.productRespository = productRespository;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart(int number, int id)
		{
			AddToCartFunction(number);
			
			return RedirectToAction("Detail", "Product", new { id = id });
		}

		//---------- Other functions ----------

		void AddToCartFunction(int number)
		{
			var cart = GetCartFromCookies();

			var detail = cart.OrderDetails.SingleOrDefault(o => o.ProductId == id);
			if (detail != null)
			{
				detail.Quantity += number;
			}
			else
			{
				var product = await productRespository.GetProductByIdAsync(id);
				if (product == null)
					return NotFound();
				cart.OrderDetails.Add(new OrderDetail
				{
					ProductId = id,
					Quantity = number,
					UnitPrice = product.RetailPrice,
					Discount = product.RetailDiscount
				});
			}

			AddCartToCookies(cart);
		}
		
		Order GetCartFromCookies()
		{
			if (Request.Cookies["Cart"] == null)
			{
				return new Order();
			}
			else
			{
				string cartJson = Request.Cookies["Cart"];
				Order cart = JsonSerializer.Deserialize<Order>(cartJson);
				return cart;
			}
		}

		void AddCartToCookies(Order cart)
		{
			string cartJson = JsonSerializer.Serialize(cart);
			var options = new CookieOptions
			{
				// Expires after 1 month
				Expires = DateTime.Now.AddMonths(1)
			};
			Response.Cookies.Append("Cart", cartJson, options);
		}
	}
}
