using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
	public interface IOrderRepository
	{
		Task AddOrderAsync(Order order, List<OrderDetail> details);

		Task<List<Order>> GetAllOrdersFullInfoAsync();
		Task<Order?> GetOrderByIdFullInfoAsync(int id);
		Task<List<OrderDetail>> GetOrderDetailByIdFullInfoAsync(int id);

		Task AddOrderPaymentHistoryAsync(OrderPaymentHistory payment);
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

		public async Task AddOrderPaymentHistoryAsync(OrderPaymentHistory payment)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.OrderPaymentHistories.Add(payment);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<List<Order>> GetAllOrdersFullInfoAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Orders.Include(o => o.Customer).Include(o => o.OrderDetails)
					.ToListAsync();
			}
		}

		public async Task<Order?> GetOrderByIdFullInfoAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Orders.Include(o => o.Customer).Include(o => o.OrderDetails)
					.SingleOrDefaultAsync(o => o.OrderId == id);
			}
		}

		public async Task<List<OrderDetail>> GetOrderDetailByIdFullInfoAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.OrderDetails.Include(o => o.Product)
					.Where(o => o.OrderId == id).ToListAsync();
			}
		}
	}
}
