using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.DataAccess.Data;

namespace VBookHaven.DataAccess.Respository
{
	public interface IShippingInfoRepository
	{
        Task<List<ShippingInfo>> GetAllShipInfoAsync();
		Task<List<ShippingInfo>> GetAllShippingInfosByCustomerIdAsync(int customerId);
		Task<ShippingInfo?> GetShippingInfoByIdAsync(int id);

		Task<int> AddShippingInfoAsync(ShippingInfo shippingInfo);
	
		//HungLM
        Task<List<ShippingInfo>?> GetAllShipInfoByCusIDAsync(int cusID);
        Task<ShippingInfo?> GetShipInfoByCusIdAndShipInfoIdAsync(int customerId, int Id);
        Task UpdateShipInfoAsync(ShippingInfo shippingInfo);
        Task DeleteShipInfoAsync(ShippingInfo shippingInfo);
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

        public async Task<ShippingInfo?> GetShipInfoByCusIdAndShipInfoIdAsync(int customerId, int Id)
        {
            return await _dbContext.ShippingInfos.Include(c => c.Customers).SingleOrDefaultAsync(x => x.ShipInfoId == Id && x.Customer.CustomerId == customerId);
        }

        public async Task UpdateShipInfoAsync(ShippingInfo obj)
        {
            var objFromDb = await _dbContext.ShippingInfos.FirstOrDefaultAsync(u => u.ShipInfoId == obj.ShipInfoId);
            if (objFromDb != null)
            {
                objFromDb.CustomerName = obj.CustomerName;
				objFromDb.Phone = obj.Phone;
                objFromDb.ShipAddress = obj.ShipAddress;
                objFromDb.ProvinceCode = obj.ProvinceCode;
                objFromDb.DistrictCode = obj.DistrictCode;
                objFromDb.WardCode = obj.WardCode;
                objFromDb.Province = obj.Province;
                objFromDb.District = obj.District;
                objFromDb.Ward = obj.Ward;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteShipInfoAsync(ShippingInfo shippingInfo)
        {
            _dbContext.ShippingInfos.Remove(shippingInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ShippingInfo>> GetAllShipInfoAsync()
        {
            using (var dbContext = new VBookHavenDBContext())
            {
                return await dbContext.ShippingInfos.ToListAsync();
            }
        }
    }
}
