using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using VBookHaven.Models;
using VBookHaven.Utility;

namespace VBookHaven.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly VBookHavenDBContext _db;
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
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Staff)).GetAwaiter().GetResult();
                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    Staff = new Staff
                    {
                        FullName = "OwerFullName",
                        Dob = new DateTime(2001, 1, 1),
                        IdCard = "123456789101",
                        Address = "123 Example Street, City, Country", 
                        Phone = "0123456789",
                        Image = "\\images\\Staff\\330352f8-a5c6-4d11-8a6d-3e2f0ca09ff0.png", 
                        IsMale = true 
                    },
                    UserName = "acchunglmhe151273@gmail.com",
                    Email = "acchunglmhe151273@gmail.com",
                    EmailConfirmed = true,
                },"Pass123@").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "acchunglmhe151273@gmail.com");
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
                        Dob = new DateTime(2002, 2, 2),
                        IdCard = "123456789101",
                        Address = "123 Example Street, City, Country",
                        Phone = "0123456789",
                        Image = "\\images\\Staff\\330352f8-a5c6-4d11-8a6d-3e2f0ca09ff0.png",
                        IsMale = true
                    },
                    EmailConfirmed = true,
                }, "Pass123@").GetAwaiter().GetResult();
                ApplicationUser user1 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "storekeeper1@gmail.com");
                _userManager.AddToRoleAsync(user1, SD.Role_Staff).GetAwaiter().GetResult();
                //Staff2 - seller
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "seller1@gmail.com",
                    Email = "seller1@gmail.com",
                    Staff = new Staff
                    {
                        FullName = "seller1FullName",
                        Dob = new DateTime(2003, 3, 3),
                        IdCard = "123456789101",
                        Address = "123 Example Street, City, Country",
                        Phone = "0123456789",
                        Image = "\\images\\Staff\\3798ced9-1c32-42aa-839d-5826d14e20ef.png",
                        IsMale = true
                    },
                    EmailConfirmed = true,
                }, "Pass123@").GetAwaiter().GetResult();
                ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "seller1@gmail.com");
                _userManager.AddToRoleAsync(user2, SD.Role_Staff).GetAwaiter().GetResult();
            }
            //1.Author

            //2.Product

            //3.Stationary



            _db.SaveChangesAsync();
            return;

        }
    }
}
