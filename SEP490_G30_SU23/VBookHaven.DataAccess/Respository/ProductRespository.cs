using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Common;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Respository
{
    public interface IProductRespository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Stationery?> GetStationeryByIdAsync(int id);
		
        Task<Product?> GetProductMoreInfoByIdAsync(int id);
		Task<Book?> GetBookMoreInfoByIdAsync(int id);

        Task<Product?> GetProductByBarcodeAsync(string barcode);

		Task AddBookAsync(Product product, Book book);
        Task AddAuthorsToBookAsync(int productId, List<int> AuthorIdList);
        Task AddStationeryAsync(Product product, Stationery stationery);

        Task UpdateProductAsync(Product product);
        Task UpdateBookAsync(Book book);
        Task UpdateBookAuthorsAsync(int bookId, List<int> AuthorIdList);
        Task UpdateStationeryAsync(Stationery stationery);

        Task<bool> ChangeStatusProductAsync(int id);
    }

    public class ProductRespository : IProductRespository
    {
        public async Task AddAuthorsToBookAsync(int productId, List<int> AuthorIdList)
        {
            using (var dbContext = new VBookHavenDBContext())
            {
				var book = await dbContext.Books.FindAsync(productId);
				if (book == null) return;

				foreach (int id in AuthorIdList)
				{
					var author = await dbContext.Authors.FindAsync(id);
					if (author != null)
						book.Authors.Add(author);
				}
				await dbContext.SaveChangesAsync();
			}
		}

        public async Task AddBookAsync(Product product, Book book)
        {
			using (var dbContext = new VBookHavenDBContext())
            {
				dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                if (product.Barcode == null)
                    product.Barcode = "PVN" + product.ProductId;

                book.ProductId = product.ProductId;
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();
			}
        }

        public async Task AddStationeryAsync(Product product, Stationery stationery)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                if (product.Barcode == null)
                    product.Barcode = "PVN" + product.ProductId;

                stationery.ProductId = product.ProductId;
                dbContext.Stationeries.Add(stationery);
                await dbContext.SaveChangesAsync();
			}
        }

        public async Task<bool> ChangeStatusProductAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                var product = await dbContext.Products.FindAsync(id);
                if (product == null)
                    return false;

                product.Status = !product.Status;
                await dbContext.SaveChangesAsync();

                return true;
			}
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Products.ToListAsync();
			}
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Books.FindAsync(id);
			}
        }

        public async Task<Book?> GetBookMoreInfoByIdAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Books.Include(b => b.Authors)
                    .SingleOrDefaultAsync(b => b.ProductId == id);
			}
		}

        public async Task<Product?> GetProductByIdAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Products.FindAsync(id);
			}
        }

        public async Task<Product?> GetProductMoreInfoByIdAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Products.Include(p => p.SubCategory).Include(p => p.Images)
                    .SingleOrDefaultAsync(p => p.ProductId == id);
			}
		}

        public async Task<Stationery?> GetStationeryByIdAsync(int id)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Stationeries.FindAsync(id);
			}
        }

        public async Task<Product?> GetProductByBarcodeAsync(string barcode)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                return await dbContext.Products.FirstOrDefaultAsync(p => p.Barcode.Equals(barcode));
			}
        }

        public async Task UpdateBookAsync(Book book)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                dbContext.Entry(book).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
			}
        }

        public async Task UpdateBookAuthorsAsync(int bookId, List<int> AuthorIdList)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                var book = await dbContext.Books.Include(b => b.Authors).SingleOrDefaultAsync(b => b.ProductId == bookId);
                if (book == null) return;

                book.Authors.Clear();
                foreach (int i in AuthorIdList)
                {
                    var author = await dbContext.Authors.FindAsync(i);
                    if (author != null)
                        book.Authors.Add(author);
                }
                await dbContext.SaveChangesAsync();
			}
        }

        public async Task UpdateProductAsync(Product product)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                dbContext.Entry(product).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
			}
			
            
		}

        public async Task UpdateStationeryAsync(Stationery stationery)
        {
			using (var dbContext = new VBookHavenDBContext())
			{
                dbContext.Entry(stationery).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
			}
        }
    }
}
