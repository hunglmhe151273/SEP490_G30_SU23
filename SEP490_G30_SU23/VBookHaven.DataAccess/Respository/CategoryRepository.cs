using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
    public interface ICategoryRepository
	{
		Task<List<Category>> GetAllCategoriesAsync();
		Task<List<SubCategory>> GetAllSubCategoriesAsync();
	}

	public class CategoryRepository : ICategoryRepository
    {
        private readonly VBookHavenDBContext dbContext;
        public CategoryRepository(VBookHavenDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public Task<List<Category>> GetAllCategoriesAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
		{
				return await dbContext.SubCategories.ToListAsync();
		}
	}
}
