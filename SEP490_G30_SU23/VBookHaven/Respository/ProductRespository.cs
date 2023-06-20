using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{

    public interface IProductRespository
    {
        Task<Product> CreateProduct(Product product);
    }

    public class ProductRespository
    {
        private readonly VBookHavenDBContext _dbContext;
        public ProductRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            //To fix
            return await _dbContext.Products.FirstOrDefaultAsync();
        }

    }
}
