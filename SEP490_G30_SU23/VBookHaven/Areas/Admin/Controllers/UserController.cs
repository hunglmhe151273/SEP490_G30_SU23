using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VBookHaven.Models;
using VBookHaven.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using VBookHaven.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VBookHaven.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        //private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            //ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            //_logger = logger;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Owner)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Seller)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Storekeeper)).GetAwaiter().GetResult();
            }
            UserVM userVM = new()
            {
                RoleList = GetRoleList(),
            };
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM model, string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if(String.IsNullOrEmpty(model.Role))
            {
                model.RoleValidate = "Hãy chọn vị trí của nhân viên";
            }
            if (model.Staff_IsMale == null)
            {
                model.GenderValidate = "Hãy chọn giới tính của nhân viên";
            }
            if (model.Staff_IsMale == null|| String.IsNullOrEmpty(model.Role))
            {
                model.RoleList = GetRoleList();
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                //add user info
                //if (model.Role == null || model.Role.Equals("Customer"))
                //{
                //    //add customer info
                //    VBookHaven.Models.Customer c = new VBookHaven.Models.Customer();
                //    c.UserName = model.Customer_UserName;
                //    c.Phone = model.Customer_Phone;
                //    user.Customer = c;
                //}
                //else
                //{
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    Staff s = new Staff();
                    s.FullName = model.Staff_FullName;
                    s.Phone = model.Staff_Phone;
                    s.IdCard = model.Staff_IdCard;
                    s.IsMale = model.Staff_IsMale;
                    s.Dob = model.Staff_Dob;
                    s.Address = model.Staff_Address;
                    if (model.Staff_ImageFile != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Staff_ImageFile.FileName);
                        string staffPath = Path.Combine(wwwRootPath, @"images\staff");

                        using (var fileStream = new FileStream(Path.Combine(staffPath, fileName), FileMode.Create))
                        {
                            model.Staff_ImageFile.CopyTo(fileStream);
                        }

                        s.Image = @"\images\staff\" + fileName;
                    }
                    user.Staff = s;
                //}


                //Create account
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    if (!String.IsNullOrEmpty(model.Role))
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Xác nhận tài khoản nhân viên",
                        $"Bạn được mời làm nhân viên công ty VBookHaven ở vị trí {model.Role}. Với mật khẩu tạm thời là: {model.Password}. Để kích hoạt tài khoản bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click vào đây</a>.");
                    return RedirectToAction("RegisterStaff", "Home");
                }

                //- TO DO: Neu add khong thanh cong xoa anh vua add
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            model.RoleList = GetRoleList();
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IEnumerable<SelectListItem> GetRoleList()
        {
            return _roleManager.Roles.Where(x => x.Name != SD.Role_Customer).Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

    }
}
