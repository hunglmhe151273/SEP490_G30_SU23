using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.ViewModels;

namespace VBookHaven.Respository
{

    public interface IApplicationUserRespository
    {
        Task<ApplicationUser?> GetStaffByUIdAsync(String userId);
        Task<List<ApplicationUser?>> GetAllStaffAsync();
        Task UpdateStaffByAsync(ApplicationUser applicationUser);
    }

    public class ApplicationUserRespository : IApplicationUserRespository
    {
        private readonly VBookHavenDBContext _dbContext;

        public ApplicationUserRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ApplicationUser?>> GetAllStaffAsync()
        {
            return await _dbContext.ApplicationUsers.Include(s => s.Staff).Where(s => s.Staff != null).ToListAsync();
        }

        public async Task<ApplicationUser?> GetStaffByUIdAsync(String userId)
        {
            return await _dbContext.ApplicationUsers.Include(s => s.Staff).SingleOrDefaultAsync(a => a.Id.Equals(userId));
        }

        public async Task UpdateStaffByAsync(ApplicationUser obj)
        {
            var objFromDb = _dbContext.ApplicationUsers.Include(a => a.Staff).FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Staff.FullName = obj.Staff.FullName;
                objFromDb.Staff.Dob = obj.Staff.Dob;
                objFromDb.Staff.Address = obj.Staff.Address;
                objFromDb.Staff.IdCard = obj.Staff.IdCard;
                objFromDb.Staff.Phone = obj.Staff.Phone;
                objFromDb.Staff.IsMale = obj.Staff.IsMale;
                if (obj.Staff.Image != null)
                {
                    objFromDb.Staff.Image = obj.Staff.Image;
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
