using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.ViewModels;

namespace VBookHaven.DataAccess.Respository
{

    public interface ICustomerRespository
    {
        Task UpdateCustomerDefaultShipInfoAsync(int customerId, int defaultShippingInfoId);
    }

    public class CustomerRespository : ICustomerRespository
    {
        private readonly VBookHavenDBContext _dbContext;

        public CustomerRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateCustomerDefaultShipInfoAsync(int customerId, int defaultShippingInfoId)
        {
            var objFromDb = _dbContext.Customers.FirstOrDefault(u => u.CustomerId == customerId);
            if (objFromDb != null)
            {
                objFromDb.DefaultShippingInfoId = defaultShippingInfoId;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
