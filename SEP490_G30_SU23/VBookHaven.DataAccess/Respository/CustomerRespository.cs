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
        Task UpdateCustomerProfile(Customer c);

        Task<List<Customer>> GetAllNoAccountCustomersAsync();
        Task<List<ShippingInfo>> GetAllNoAccountShippingInfoAsync();
        Task AddCustomerNoAccountAsync(Customer customer);
    }

    public class CustomerRespository : ICustomerRespository
    {
        private readonly VBookHavenDBContext _dbContext;
        public CustomerRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomerNoAccountAsync(Customer customer)
        {
            using (var dbContext = new VBookHavenDBContext())
            {
                dbContext.Customers.Add(customer);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Customer>> GetAllNoAccountCustomersAsync()
        {
            using (var dbContext = new VBookHavenDBContext())
            {
                return await dbContext.Customers.Where(c => c.AccountId == null)
                    .Include(c => c.DefaultShippingInfo).ToListAsync();
            }
        }

        public async Task<List<ShippingInfo>> GetAllNoAccountShippingInfoAsync()
        {
			using (var dbContext = new VBookHavenDBContext())
            {
                var shipInfo = await dbContext.ShippingInfos.Include(s => s.Customer)
                    .Where(s => s.Customer != null).Where(s => s.Customer.AccountId == null)
                    .ToListAsync();

                return shipInfo;
            }    
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
                    
                    }
                }
        }

        public async Task UpdateCustomerProfile(Customer obj)
        {
            var objFromDb = await _dbContext.Customers.FirstOrDefaultAsync(u => u.CustomerId == obj.CustomerId);
            if (objFromDb != null)
            {
                objFromDb.Phone = obj.Phone;
                objFromDb.DOB = obj.DOB;
                objFromDb.IsMale = obj.IsMale;
                objFromDb.FullName = obj.FullName;
                if(obj.Image != null)
                {
                    objFromDb.Image = obj.Image;
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
