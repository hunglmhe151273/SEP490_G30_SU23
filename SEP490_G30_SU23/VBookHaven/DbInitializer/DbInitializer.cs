using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VBookHaven.Models;
using VBookHaven.Utility;

namespace VBookHaven.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly VBookHavenDBContext _db;
        private String password = "pass123@";
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            VBookHavenDBContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {

            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }
            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Owner)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Seller)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Storekeeper)).GetAwaiter().GetResult();
                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    Staff = new Staff
                    {
                        FullName = "OwerFullName",
                    },
                    UserName = "hunglmhe151273@fpt.edu.vn",
                    Email = "hunglmhe151273@fpt.edu.vn",
                    EmailConfirmed = true,
                },"Pass123@").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "hunglmhe151273@fpt.edu.vn");
                _userManager.AddToRoleAsync(user, SD.Role_Owner).GetAwaiter().GetResult();
            }
            if (_db.Staff.Count() < 2)
            {
                //Staff1 - storekeeper
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "storekeeper1@gmail.com",
                    Email = "storekeeper1@gmail.com",
                    Staff = new Staff
                    {
                        FullName = "storekeeper1FullName",
                    },
                    EmailConfirmed = true,
                }, "Pass123@").GetAwaiter().GetResult();
                ApplicationUser user1 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "storekeeper1@gmail.com");
                _userManager.AddToRoleAsync(user1, SD.Role_Owner).GetAwaiter().GetResult();
                //Staff2 - seller
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "seller1@gmail.com",
                    Email = "seller1@gmail.com",
                    Staff = new Staff
                    {
                        FullName = "seller1FullName",
                    },
                    EmailConfirmed = true,
                }, "Pass123@").GetAwaiter().GetResult();
                ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "seller1@gmail.com");
                _userManager.AddToRoleAsync(user2, SD.Role_Owner).GetAwaiter().GetResult();
            }
            //Create product

            _db.SaveChangesAsync();
            return;

        }
    }
}
