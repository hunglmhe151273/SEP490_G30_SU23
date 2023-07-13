using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.DataAccess.Data;

namespace VBookHaven.DataAccess.Respository
{
	public interface IShippingInfoRepository
	{
		Task<List<ShippingInfo>> GetAllShippingInfosByCustomerIdAsync(int customerId);
		Task<ShippingInfo?> GetShippingInfoByIdAsync(int id);

		Task<int> AddShippingInfoAsync(ShippingInfo shippingInfo);
	}

	public class ShippingInfoRepository : IShippingInfoRepository
	{
		public async Task<int> AddShippingInfoAsync(ShippingInfo shippingInfo)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.ShippingInfos.Add(shippingInfo);
				await dbContext.SaveChangesAsync();

				return shippingInfo.ShipInfoId;
			}
		}

		public async Task<List<ShippingInfo>> GetAllShippingInfosByCustomerIdAsync(int customerId)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.ShippingInfos.Where(s => s.CustomerId == customerId).ToListAsync();
			}
		}

		public async Task<ShippingInfo?> GetShippingInfoByIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.ShippingInfos.FindAsync(id);
			}
		}
	}
}
