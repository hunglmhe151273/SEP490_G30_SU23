using VBookHaven.Models;

namespace VBookHaven.Common
{
	public static class CommonGetCode
	{
		private static VBookHavenDBContext dBContext = new VBookHavenDBContext();

		public static List<SubCategory> GetAllSubCategories()
		{
			List<SubCategory> subCategories = new List<SubCategory>();
			subCategories = dBContext.SubCategories.Where(s => s.Status == true).ToList();

			return subCategories;
		}

		public static List<Author> GetAllAuthors()
		{
			List<Author> authors = new List<Author>();
			authors = dBContext.Authors.Where(a => a.Status == true).ToList();

			return authors;
		}
	}
}
