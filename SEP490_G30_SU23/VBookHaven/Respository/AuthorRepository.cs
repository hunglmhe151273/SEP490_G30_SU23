using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
	public interface IAuthorRepository
	{
		Task<List<Author>> GetAllAuthorsAsync();
		Task<Author?> GetAuthorByIdAsync(int id);
		Task<List<Author>> GetAuthorsByBookIdAsync(int id);

		Task AddAuthorAsync(Author author);
		Task UpdateAuthorAsync(Author author);
	}
	
	public class AuthorRepository : IAuthorRepository
	{
		public async Task AddAuthorAsync(Author author)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Authors.Add(author);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<List<Author>> GetAllAuthorsAsync()
		{
			using (var dbContext = new VBookHavenDBContext())
			{ 
				return await dbContext.Authors.ToListAsync(); 
			}
		}

		public async Task<Author?> GetAuthorByIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Authors.FindAsync(id);
			}
		}

		public async Task<List<Author>> GetAuthorsByBookIdAsync(int id)
		{
			var authorsId = new List<int>();

			using (var dbContext = new VBookHavenDBContext())
			{
				var book = await dbContext.Books.Include(b => b.Authors)
					.SingleOrDefaultAsync(b => b.ProductId == id);
				if (book != null)
					return book.Authors.ToList();
				else return new List<Author>();
			}
		}

		public async Task UpdateAuthorAsync(Author author)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Entry(author).State = EntityState.Modified;
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
