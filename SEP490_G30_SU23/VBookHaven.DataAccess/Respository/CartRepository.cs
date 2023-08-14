using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.DataAccess.Data;

namespace VBookHaven.DataAccess.Respository
{
	public interface ICartRepository
	{
		Task<List<CartDetail>> GetCartByCustomerIdAsync(int customerId);
		Task<CartDetail?> GetCartItemAsync(int customerId, int productId);

		Task AddItemToCartAsync(CartDetail item);
		Task AddCartFromCookieToDbAsync(List<CartDetail> items);

		Task UpdateCartAsync(CartDetail item);
		Task UpdateCartMultipleAsync(List<CartDetail> items);

		Task DeleteItemFromCartAsync(int customerId, int productId);
		Task ClearCartByCustomerIdAsync(int customerId);
	}

	public class CartRepository : ICartRepository
	{
		public async Task AddItemToCartAsync(CartDetail item)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.CartDetails.Add(item);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task AddCartFromCookieToDbAsync(List<CartDetail> items)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				foreach (var item in items)
				{
					var existedItem = await dbContext.CartDetails.FindAsync(item.CustomerId, item.ProductId);
					if (existedItem != null)
					{
						existedItem.Quantity += item.Quantity;
					}
					else dbContext.CartDetails.Add(item);
				}
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteItemFromCartAsync(int customerId, int productId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				var item = await dbContext.CartDetails.FindAsync(customerId, productId);
				if (item != null)
				{
					dbContext.CartDetails.Remove(item);
					await dbContext.SaveChangesAsync();
				}
			}
		}

		public async Task<List<CartDetail>> GetCartByCustomerIdAsync(int customerId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				var cart = await dbContext.CartDetails.Include(c => c.Product)
					.Where(c => c.CustomerId == customerId).ToListAsync();
				return cart;
			}
		}

		public async Task UpdateCartAsync(CartDetail item)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Entry(item).State = EntityState.Modified;
				await dbContext.SaveChangesAsync();
			}
		}

		public Task UpdateCartMultipleAsync(List<CartDetail> items)
		{
			throw new NotImplementedException();
		}

		public async Task ClearCartByCustomerIdAsync(int customerId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				var cart = await dbContext.CartDetails.Where(c => c.CustomerId == customerId).ToListAsync();
				dbContext.CartDetails.RemoveRange(cart);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<CartDetail?> GetCartItemAsync(int customerId, int productId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.CartDetails.FindAsync(customerId, productId);
			}
		}
	}
}
