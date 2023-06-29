using Microsoft.EntityFrameworkCore;
using VBookHaven.Common;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
    public interface IProductRespository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Stationery?> GetStationeryByIdAsync(int id);

        Task AddBookAsync(Product product, Book book);
        Task AddStationeryAsync(Product product, Stationery stationery);

        Task UpdateProductAsync(Product product);
        Task UpdateBookAsync(Book book);
        Task UpdateStationeryAsync(Stationery stationery);
    }

    public class ProductRespository : IProductRespository
    {
        private readonly VBookHavenDBContext dbContext;

        public ProductRespository(VBookHavenDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAsync(Product product, Book book)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

			if (product.Barcode == null)
				product.Barcode = "PVN" + product.ProductId;

			book.ProductId = product.ProductId;
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddStationeryAsync(Product product, Stationery stationery)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

			if (product.Barcode == null)
				product.Barcode = "PVN" + product.ProductId;

			stationery.ProductId = product.ProductId;
            dbContext.Stationeries.Add(stationery);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await dbContext.Books.FindAsync(id);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<Stationery?> GetStationeryByIdAsync(int id)
        {
            return await dbContext.Stationeries.FindAsync(id);
        }

        public async Task UpdateBookAsync(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
			await dbContext.SaveChangesAsync();
		}

        public async Task UpdateStationeryAsync(Stationery stationery)
        {
            dbContext.Entry(stationery).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
