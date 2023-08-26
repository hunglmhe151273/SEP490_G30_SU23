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
		
		Task<List<Order>> GetAllOrdersByCustomerAsync(int customerId);
		Task<List<OrderDetail>> GetAllOrderDetailByCustomerAllInfoAsync(int customerId);

		Task UpdateOrderAsync(Order order);

		Task AddOrderPaymentHistoryAsync(OrderPaymentHistory payment);
		Task<List<OrderPaymentHistory>> GetOrderPaymentHistoryByIdFullInfoAsync(int orderId);
		Task<int?> GetOrderTotalCostAsync(int orderId);

		Task<List<Staff>> GetAllStaffAsync();
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
				{
					detail.OrderId = order.OrderId;

					var product = await dbContext.Products.FindAsync(detail.ProductId);
					product.AvailableUnit -= detail.Quantity;
					dbContext.Entry(product).State = EntityState.Modified;
				}

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

		public async Task<List<OrderDetail>> GetAllOrderDetailByCustomerAllInfoAsync(int customerId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.OrderDetails.Include(o => o.Order).Include(o => o.Product)
					.Where(o => o.Order.CustomerId == customerId).ToListAsync();
			}
		}

		public async Task<List<Order>> GetAllOrdersByCustomerAsync(int customerId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
			}
		}

		public async Task<List<Order>> GetAllOrdersFullInfoAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Orders.Include(o => o.Customer).Include(o => o.OrderDetails)
					.Include(o => o.Staff).ToListAsync();
			}
		}

		public async Task<List<Staff>> GetAllStaffAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Staff.ToListAsync();
			}	
		}

		public async Task<Order?> GetOrderByIdFullInfoAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Orders.Include(o => o.Customer).Include(o => o.OrderDetails)
					.Include(o => o.Staff).SingleOrDefaultAsync(o => o.OrderId == id);
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

		public async Task<List<OrderPaymentHistory>> GetOrderPaymentHistoryByIdFullInfoAsync(int orderId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				var payments = await dbContext.OrderPaymentHistories.Where(o => o.OrderId == orderId)
					.Include(o => o.Staff).ToListAsync();
				return payments.OrderBy(p => p.PaymentDate).ToList();
			}
		}

		public async Task<int?> GetOrderTotalCostAsync(int orderId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				var order = await dbContext.Orders.FindAsync(orderId);
				if (order == null)
					return null;

				var details = await dbContext.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();

				double total = 0;
				foreach (var d in details)
				{
					total += d.UnitPrice.Value * (1 - d.Discount.Value / 100) * d.Quantity.Value;
				}
				total *= (1 + order.VAT.Value / 100);

				return (int)Math.Ceiling(total);
			}
		}

		public async Task UpdateOrderAsync(Order order)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Entry(order).State = EntityState.Modified;
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
