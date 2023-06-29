using Microsoft.EntityFrameworkCore;
using VBookHaven.Common;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
    public interface IProductRespository
    {
        Task<List<Product>> GetAllProducts();
        Task<List<SubCategory>> GetAllSubCategories();
        Task<List<Author>> GetAllAuthors();
    }

    public class ProductRespository : IProductRespository
    {
        private readonly VBookHavenDBContext dbContext;

        public ProductRespository(VBookHavenDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            return await dbContext.SubCategories.ToListAsync();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await dbContext.Authors.ToListAsync();
        }
    }
}
