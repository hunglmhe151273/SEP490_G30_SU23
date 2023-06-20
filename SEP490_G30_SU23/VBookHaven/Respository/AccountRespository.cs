using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;

namespace VBookHaven.Respository
{
    public interface IAccountRespository
    {
        Task<Models.Account> findByEmailAndPassword(string email, string password);
    }
    public class AccountRespository : IAccountRespository
    {

        private readonly VBookHavenDBContext _dbContext;
        public AccountRespository(VBookHavenDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Account> findByEmailAndPassword(String email, String password)
        {
                var acc = await _dbContext.Accounts.Where(u => u.Password.Equals(password) && u.Username.Equals(email)).FirstOrDefaultAsync();
                return acc;
        }
    }


}
