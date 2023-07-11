using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
	public interface ICartRepository
	{
		Task<List<CartDetail>> GetCartByCustomerIdAsync(int customerId);

		Task AddItemToCartAsync(CartDetail item);
		Task AddOrUpdateMultipleItemsToCartAsync(List<CartDetail> items);

		Task UpdateCartAsync(CartDetail item);
		Task UpdateCartMultipleAsync(List<CartDetail> items);

		Task DeleteItemFromCartAsync(int customerId, int productId);
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

		public async Task AddOrUpdateMultipleItemsToCartAsync(List<CartDetail> items)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				foreach (var item in items)
				{
					dbContext.Entry(item).State = 
						await dbContext.CartDetails.FindAsync(item.ProductId, item.CustomerId) == null 
						? EntityState.Added
						: EntityState.Modified;
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
				var cart = await dbContext.CartDetails.Where(c => c.CustomerId == customerId)
					.ToListAsync();
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
	}
}
