using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.DataAccess.Respository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Crypto.Generators;

namespace VBookHaven.Areas.Customer.Controllers
{
	// Giau inactivated product
	// Khi dat hang -> Chi cho max la so luong con lai

	// Trang detail product - Khi chuyen anh khac va zoom - bi cat mat mot it ben phai?
	// Chua co list view cua trang product list
	// Recommended new product o ben trai trang index

	public class ProductBuyingViewModel
	{
		public List<Product> Products { get; set; }
		public List<Category> Categories { get; set; }
		public Dictionary<int, string?> Thumbnails { get; set; }

		public string SearchText { get; set; }
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; }
		public List<int> SelectedSubCategories { get; set; }
		public List<int> SelectedCategories { get; set; }
		public SelectList SortList { get; set; }

		public int Page { get; set; }
		public int TotalPage { get; set; }
		public int First { get; set; }
		public int Last { get; set; }
		public int Total { get; set; }

		public ProductBuyingViewModel()
		{
			Products = new List<Product>();
			Categories = new List<Category>();
			Thumbnails = new Dictionary<int, string?>();

			SearchText = "";
			MinPrice = 0;
			MaxPrice = 1000000;
			SelectedSubCategories = new List<int>();
			SelectedCategories = new List<int>();

			var sortList = new List<object>()
			{
				new { Value = "newest", Text = "Sản phẩm mới nhất" },
				new { Value = "oldest", Text = "Sản phẩm cũ nhất" },
				new { Value = "price-asc", Text = "Giá thấp -> cao" },
				new { Value = "price-desc", Text = "Giá cao -> thấp"}
			};
			SortList = new SelectList(sortList, "Value", "Text", "newest");

			Page = 1;
			TotalPage = 1;
			First = 0;
			Last = 0;
			Total = 0;
		}
	}

	public class ProductDetailViewModel
	{
		public Product Product { get; set; }
		public Book Book { get; set; }
		public Stationery Stationery { get; set; }
		public List<Image> Images { get; set; }

		public ProductDetailViewModel()
		{
			Product = new Product();
			Book = new Book();
			Stationery = new Stationery();
			Images = new List<Image>();
		}
	}
	
	[Area("Customer")]
	public class ProductController : Controller
	{
		private readonly IProductRespository productRespository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IAuthorRepository authorRepository;
		private readonly IImageRepository imageRepository;

		public ProductController(IProductRespository productRespository, ICategoryRepository categoryRepository, 
			IAuthorRepository authorRepository, IImageRepository imageRepository)
		{
			this.productRespository = productRespository;
			this.categoryRepository = categoryRepository;
			this.authorRepository = authorRepository;
			this.imageRepository = imageRepository;
		}

		public async Task<IActionResult> Index(string? search, int? minPrice, int? maxPrice, 
			List<int> subCat, string? sort, int? page)
		{
			var model = new ProductBuyingViewModel();
			int productPerPage = 9;

			var productsTask = productRespository.GetAllProductsAsync();
			var categoriesTask = categoryRepository.GetAllCategoriesAllInfoAsync();

			model.Products = await productsTask;

			if (search != null)
			{
				model.Products = model.Products
					.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();
				model.SearchText = search;
			}

			if (minPrice != null)
			{
				model.Products = model.Products
					.Where(p => GetPriceAfterDiscount(p) >= minPrice).ToList();
				model.MinPrice = minPrice.Value;
			}

			if (maxPrice != null)
			{
				model.Products = model.Products
					.Where(p => GetPriceAfterDiscount(p) <= maxPrice).ToList();
				model.MaxPrice = maxPrice.Value;
			}

			if (subCat.Count > 0)
			{
				model.Products = model.Products.Where(p => subCat.Contains(p.SubCategoryId.Value)).ToList();
				model.SelectedSubCategories = subCat;
				foreach (var subId in subCat)
				{
					var sub = await categoryRepository.GetSubCategoryById(subId);
					if (sub != null)
						model.SelectedCategories.Add(sub.CategoryId.Value);
				}
			}
			else
			{
				var catList = await categoriesTask;
				foreach (var cat in catList)
				{
					model.SelectedCategories.Add(cat.CategoryId);
				}
			}

			if (sort == null)
				sort = "newest";
			var selected = model.SortList.FirstOrDefault(s => s.Value.Equals(sort));
			if (selected != null)
				selected.Selected = true;
			switch (sort)
			{
				case "newest":
					model.Products.Sort((x, y) => -x.ProductId.CompareTo(y.ProductId));
					break;
				case "oldest":
					model.Products.Sort((x, y) => x.ProductId.CompareTo(y.ProductId));
					break;
				case "price-asc":
					model.Products.Sort((x, y) => GetPriceAfterDiscount(x).CompareTo(GetPriceAfterDiscount(y)));
					break;
				case "price-desc":
					model.Products.Sort((x, y) => -GetPriceAfterDiscount(x).CompareTo(GetPriceAfterDiscount(y)));
					break;
			}

			if (page == null)
				page = 1;
			var count = model.Products.Count;
			int totalPage, first, last;
			if (count == 0)
			{
				totalPage = 1;
				first = 0;
				last = 0;
			}
			else
			{
				totalPage = (count - 1) / productPerPage + 1;

				if (page < 1)
					page = 1;
				if (page > totalPage)
					page = totalPage;

				first = (page.Value - 1) * productPerPage + 1;
				last = Math.Min(page.Value * productPerPage, count);

				model.Products = model.Products.GetRange(first - 1, last - first + 1);
			}
			model.Page = page.Value;
			model.TotalPage = totalPage;
			model.First = first;
			model.Last = last;
			model.Total = count;

			foreach (Product p in model.Products)
			{
				var thumbnail = await imageRepository.GetThumbnailByProductIdAsync(p.ProductId);
				string? name;
				if (thumbnail != null)
					name = thumbnail.ImageName;
				else name = null;
				model.Thumbnails.Add(p.ProductId, name);
			}

			model.Categories = await categoriesTask;

			return View(model);
		}

		public async Task<IActionResult> Detail(int id)
		{
			var model = new ProductDetailViewModel();

			var product = await productRespository.GetProductMoreInfoByIdAsync(id);
			if (product == null)
				return NotFound();

			model.Product = product;
			if (product.IsBook == true)
			{
				model.Book = await productRespository.GetBookMoreInfoByIdAsync(id);
			}
			else
			{
				model.Stationery = await productRespository.GetStationeryByIdAsync(id);
			}

			foreach (Image i in model.Product.Images)
			{
				if (i.Status == true)
					model.Images.Add(i);
			}

			return View(model);
		}

		//---------- Other functions ----------

		int GetPriceAfterDiscount(Product p)
		{
			return (int)(p.RetailPrice.Value * (1 - p.RetailDiscount.Value / 100));
		}
	}
}
