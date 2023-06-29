using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
	public interface ICategoryRepository
	{
		Task<List<Category>> GetAllCategoriesAsync();
		Task<List<SubCategory>> GetAllSubCategoriesAsync();
	}

	public class CategoryRepository : ICategoryRepository
	{
		private readonly VBookHavenDBContext dbContext;

		public CategoryRepository(VBookHavenDBContext dbContext)
		{
			this.dbContext = dbContext;
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
