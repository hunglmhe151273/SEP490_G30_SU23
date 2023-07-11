using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;

namespace VBookHaven.DataAccess.Common
{
    public class CommonGetCode
	{
		private VBookHavenDBContext dBContext;
		public CommonGetCode()
		{
			dBContext = new VBookHavenDBContext();
		}

		public List<SubCategory> GetAllSubCategories()
		{
			List<SubCategory> subCategories = new List<SubCategory>();
			subCategories = dBContext.SubCategories.ToList();

			return subCategories;
		}

		public List<Author> GetAllAuthors()
		{
			List<Author> authors = new List<Author>();
			authors = dBContext.Authors.ToList();

			return authors;
		}

		public List<Product> GetAllProduct()
		{
			List<Product> products = new List<Product>();
			products = dBContext.Products.ToList();

			return products;
		}

		public Author? GetAuthorById(int id)
		{
			var author = dBContext.Authors.SingleOrDefault(a => a.AuthorId == id);
			return author;
		}

		public List<Author> GetAuthorsByBookId(int id)
		{
			var authors = new List<Author>();
			
			var book = dBContext.Books.Include(b => b.Authors).SingleOrDefault(p => p.ProductId == id);
			if (book != null)
				authors = book.Authors.ToList();

			return authors;
		}

		public Product? GetProductById(int id)
		{
			var product = dBContext.Products.SingleOrDefault(p => p.ProductId == id);
			return product;
		}

		public Book? GetBookById(int id)
		{
			var book = dBContext.Books.SingleOrDefault(b => b.ProductId == id);
			return book;
		}

		public Stationery? GetStationeryById(int id)
		{
			var stationery = dBContext.Stationeries.SingleOrDefault(s => s.ProductId == id);
			return stationery;
		}

		public Image? GetImageById(int id)
		{
			var image = dBContext.Images.SingleOrDefault(s => s.ImageId == id);
			return image;
		}

		public List<Image> GetImagesByProductId(int id)
		{
			var images = new List<Image>();

			images = dBContext.Images.Where(i => i.ProductId == id && i.Status == true).ToList();

			return images;
		}
	}
}
