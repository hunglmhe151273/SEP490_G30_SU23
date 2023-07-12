using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
	public interface IOrderRepository
	{
		Task AddOrderAsync(Order order, List<OrderDetail> details);
	}

	public class OrderRepository : IOrderRepository
	{
		public async Task AddOrderAsync(Order order, List<OrderDetail> details)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Orders.Add(order);
				await dbContext.SaveChangesAsync();

				foreach (var detail in details)
					detail.OrderId = order.OrderId;

				dbContext.OrderDetails.AddRange(details);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
