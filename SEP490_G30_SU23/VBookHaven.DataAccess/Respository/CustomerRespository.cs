using Microsoft.EntityFrameworkCore;
using System.Numerics;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.Models.ViewModels;
using VBookHaven.ViewModels;

namespace VBookHaven.DataAccess.Respository
{

    public interface ICustomerRespository
    {
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task UpdateCustomerDefaultShipInfoAsync(int customerId, int defaultShippingInfoId);
        Task UpdateCustomerDefaultShipInfoOnCreateAsync(ShippingInfoVM model);
    }

    public class CustomerRespository : ICustomerRespository
    {
        private readonly VBookHavenDBContext _dbContext;
        public CustomerRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _dbContext.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId);
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

        public async Task UpdateCustomerDefaultShipInfoOnCreateAsync(ShippingInfoVM model)
        {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                    model.ShippingInfo.CustomerId = model.CustomerId;
                    var newShipInfo = model.ShippingInfo;
                    _dbContext.ShippingInfos.Add(newShipInfo);
                    await _dbContext.SaveChangesAsync(); // lưu đối tượng vào cơ sở dữ liệu
                    var shipInfoId = newShipInfo.ShipInfoId;
                    // Thực hiện update customer
                    var objFromDb = _dbContext.Customers.FirstOrDefault(u => u.CustomerId == model.CustomerId);
                    if (objFromDb != null)
                    {
                        objFromDb.DefaultShippingInfoId = newShipInfo.ShipInfoId;
                    }
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Không cần gọi transaction.Rollback() ở đây, vì transaction sẽ được rollback tự động khi thoát khỏi using scope
                    }
                }
        }
    }
}
