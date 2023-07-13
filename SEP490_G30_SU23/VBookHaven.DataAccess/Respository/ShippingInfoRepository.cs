using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.DataAccess.Data;

namespace VBookHaven.DataAccess.Respository
{
	public interface IShippingInfoRepository
	{
		Task<List<ShippingInfo>> GetAllShippingInfosByCustomerIdAsync(int customerId);
		Task<ShippingInfo?> GetShippingInfoByIdAsync(int id);

		Task AddShippingInfoAsync(ShippingInfo shippingInfo);
		//HungLM
        Task<List<ShippingInfo>?> GetAllShipInfoByCusIDAsync(int cusID);
        Task<ShippingInfo?> GetShipInfoByIdAsync(int customerId, int Id);
        Task UpdateShipInfoAsync(ShippingInfo shippingInfo);
    }

	public class ShippingInfoRepository : IShippingInfoRepository
	{
		public async Task AddShippingInfoAsync(ShippingInfo shippingInfo)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.ShippingInfos.Add(shippingInfo);
				await dbContext.SaveChangesAsync();
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
        //HungLM
        private readonly VBookHavenDBContext _dbContext;

        public ShippingInfoRepository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ShippingInfo>?> GetAllShipInfoByCusIDAsync(int cusID)
        {
            return await _dbContext.ShippingInfos.Include(x => x.Customers).Where(s => s.CustomerId == cusID).ToListAsync();
        }

        public async Task<ShippingInfo?> GetShipInfoByIdAsync(int customerId, int Id)
        {
            return await _dbContext.ShippingInfos.Include(c => c.Customer).SingleOrDefaultAsync(x => x.ShipInfoId == Id && x.Customer.CustomerId == customerId);
        }

        public async Task UpdateShipInfoAsync(ShippingInfo obj)
        {
            var objFromDb = _dbContext.ShippingInfos.FirstOrDefault(u => u.ShipInfoId == obj.ShipInfoId);
            if (objFromDb != null)
            {
                objFromDb.CustomerName = obj.CustomerName;
				objFromDb.Phone = obj.Phone;
                objFromDb.ShipAddress = obj.ShipAddress;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
