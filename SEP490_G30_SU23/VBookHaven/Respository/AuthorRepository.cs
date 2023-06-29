using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
	public interface IAuthorRepository
	{
		Task<List<Author>> GetAllAuthorsAsync();
		Task<Author?> GetAuthorByIdAsync(int id);
		Task AddAuthorAsync(Author author);
		Task UpdateAuthorAsync(Author author);
	}
	
	public class AuthorRepository : IAuthorRepository
	{
		private readonly VBookHavenDBContext dbContext;

		public AuthorRepository(VBookHavenDBContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddAuthorAsync(Author author)
		{
			dbContext.Authors.Add(author);
			await dbContext.SaveChangesAsync();
		}

		public async Task<List<Author>> GetAllAuthorsAsync()
		{
			return await dbContext.Authors.ToListAsync();
		}

		public async Task<Author?> GetAuthorByIdAsync(int id)
		{
			return await dbContext.Authors.FindAsync(id);
		}

		public async Task UpdateAuthorAsync(Author author)
		{
			dbContext.Entry(author).State = EntityState.Modified;
			await dbContext.SaveChangesAsync();
		}
	}
}
