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
        public Task<List<Category>> GetAllCategoriesAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.SubCategories.ToListAsync();
			}
		}
	}
}
