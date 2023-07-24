using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
    public interface ICategoryRepository
	{
		Task<List<Category>> GetAllCategoriesAsync();
		Task<List<Category>> GetAllCategoriesAllInfoAsync();
		Task<List<SubCategory>> GetAllSubCategoriesAsync();
		Task<List<SubCategory>> GetAllSubCategoriesByCategoryIdAsync(int id);
		Task<SubCategory?> GetSubCategoryById(int id);
	}

	public class CategoryRepository : ICategoryRepository
    {
		public async Task<List<Category>> GetAllCategoriesAllInfoAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Categories.Include(c => c.SubCategories).ToListAsync();
			}
		}

		public async Task<List<Category>> GetAllCategoriesAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Categories.ToListAsync();
			}
		}

		public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.SubCategories.ToListAsync();
			}
		}

		public async Task<List<SubCategory>> GetAllSubCategoriesByCategoryIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.SubCategories.Where(s => s.CategoryId == id).ToListAsync();
			}
		}

		public async Task<SubCategory?> GetSubCategoryById(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.SubCategories.FindAsync(id);
			}
		}
	}
}
