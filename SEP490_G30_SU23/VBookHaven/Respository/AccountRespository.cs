using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.Services;

namespace VBookHaven.Respository
{
    public interface IAccountRespository
    {
        public Task<Models.Account> findByEmailAndPassword(string email, string password);
        public Task ResetPassword(string userName);
    }
    public class AccountRespository : IAccountRespository
    {
        private readonly VBookHavenDBContext _dbContext;
        private readonly IEmailSender _emailSender;
        public AccountRespository(VBookHavenDBContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public async Task<Models.Account> findByEmailAndPassword(String email, String password)
        {
                var acc = await _dbContext.Accounts.Where(u => u.Password.Equals(password) && u.Username.Equals(email)).FirstOrDefaultAsync();
                return acc;
        }
        public async Task ResetPassword(string gmail)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@$%^*&";
            var user = await _dbContext.Accounts.Where(acc => acc.Username.Equals(gmail)).FirstOrDefaultAsync();
            if (user != null)
            {
                var length = random.Next(8, 13);
                var password = "";
                for (int i = 0; i < length; i++)
                {
                    password += chars[random.Next(chars.Length)];
                }
                //var name = user.LastName + " " + user.FirstName;
                _emailSender.SendForgotPasswordEmailAsync("Name", user.Username, password);

                //user.HashPassword = BCryptNet.HashPassword(password);
                user.Password = password;
                _dbContext.Accounts.Update(user);
                _dbContext.SaveChanges();
            }

           
        }
    }



}
