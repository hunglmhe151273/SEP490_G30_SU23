using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
	public interface IImageRepository
	{
		Task<Image?> GetImageByIdAsync(int id);
		Task<Image?> GetThumbnailByProductIdAsync(int id);
		Task<List<Image>> GetImagesByProductIdAsync(int id);
		
		Task AddImageAsync(Image image);

		Task DeleteImageAsync(int id);
		Task DeleteImageListAsync(List<int> idList);
		
		Task UploadImagesAsync(int productId, List<IFormFile> AddImageList);
	}

	public class ImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;

		public ImageRepository(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task AddImageAsync(Image image)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				dbContext.Images.Add(image);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteImageAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				Image? image = await dbContext.Images.FindAsync(id);
				if (image == null) return;

				image.Status = false;
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteImageListAsync(List<int> idList)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				foreach (int id in idList)
				{
					Image? image = await dbContext.Images.FindAsync(id);
					if (image != null)
						image.Status = false;
				}

				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<Image?> GetImageByIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Images
					.SingleOrDefaultAsync(i => i.ImageId == id && i.Status == true);
			}
		}

		public async Task<List<Image>> GetImagesByProductIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Images.Where(i => i.ProductId == id && i.Status == true)
					.ToListAsync();
			}
		}

		public async Task<Image?> GetThumbnailByProductIdAsync(int id)
		{
			using (var dbContext = new VBookHavenDBContext())
			{
				return await dbContext.Images
					.FirstOrDefaultAsync(i => i.ProductId == id && i.Status == true);
			}
		}

		public async Task UploadImagesAsync(int productId, List<IFormFile> AddImageList)
		{
			string wwwRootPath = webHostEnvironment.WebRootPath;
			foreach (IFormFile image in AddImageList)
			{
				if (image != null)
				{
					using (var dbContext = new VBookHavenDBContext())
					{
						Image imageInfo = new Image();
						imageInfo.Status = true;
						imageInfo.ProductId = productId;

						dbContext.Images.Add(imageInfo);
						await dbContext.SaveChangesAsync();

						string fileName = "image_" + imageInfo.ImageId + Path.GetExtension(image.FileName);
						string imagePath = Path.Combine(wwwRootPath, @"images\img");

						using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
						{
							image.CopyTo(fileStream);
						}

						imageInfo.ImageName = fileName;
						await dbContext.SaveChangesAsync();
					}
				}
			}
		}
	}
}
