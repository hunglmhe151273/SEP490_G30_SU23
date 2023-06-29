using VBookHaven.Models;

namespace VBookHaven.Respository
{
	public interface IImageRepository
	{
		Task<Image?> GetImageByIdAsync(int id);
		Task AddImageAsync(Image image);
		Task DeleteImageAsync(int id);
	}

	public class ImageRepository : IImageRepository
	{
		private readonly VBookHavenDBContext dbContext;

		public ImageRepository(VBookHavenDBContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddImageAsync(Image image)
		{
			dbContext.Images.Add(image);
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteImageAsync(int id)
		{
			Image? image = await GetImageByIdAsync(id);
			if (image == null) return;

			image.Status = false;
			await dbContext.SaveChangesAsync();
		}

		public async Task<Image?> GetImageByIdAsync(int id)
		{
			return await dbContext.Images.FindAsync(id);
		}
	}
}
