using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
	public interface IPurchaseOrderRepository
	{
		Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
	}

	public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly VBookHavenDBContext _dbContext;


        public PurchaseOrderRepository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
		{
            _dbContext.Entry(purchaseOrder).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}
	}
}
