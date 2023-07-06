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
using System.Security.Claims;
using VBookHaven.Respository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly IApplicationUserRespository _IApplicationUserRespository;
        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            //ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IApplicationUserRespository applicationUserRespository,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            //_logger = logger;
            _emailSender = emailSender;
            _IApplicationUserRespository = applicationUserRespository;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public IActionResult Create()
        {
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
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //get application user by id

            ProfileVM profileVM = new ProfileVM();
            profileVM.StaffProfileVM.ApplicationUser = await _IApplicationUserRespository.GetStaffByUIdAsync(userId);
            //view application user
            return View(profileVM);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileVM profileVM)
        {
            if (profileVM.StaffProfileVM.ApplicationUser.Staff.IsMale == null)
            {
                profileVM.StaffProfileVM.GenderValidate = "Hãy chọn giới tính của nhân viên";
                return View(profileVM);
            }
            ModelState.Clear();
			if (!TryValidateModel(profileVM.StaffProfileVM))
            {

				// view application user
				return View(profileVM);
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (profileVM.StaffProfileVM.Staff_ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileVM.StaffProfileVM.Staff_ImageFile.FileName);
                string staffPath = Path.Combine(wwwRootPath, @"images\staff");
                if (!string.IsNullOrEmpty(profileVM.StaffProfileVM.ApplicationUser.Staff.Image))
                {
                    //delete the old image
                    var oldImagePath =
                        Path.Combine(wwwRootPath, profileVM.StaffProfileVM.ApplicationUser.Staff.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //add image, update url
                using (var fileStream = new FileStream(Path.Combine(staffPath, fileName), FileMode.Create))
                {
                    profileVM.StaffProfileVM.Staff_ImageFile.CopyTo(fileStream);
                }

                profileVM.StaffProfileVM.ApplicationUser.Staff.Image = @"\images\staff\" + fileName;
            }

            //update staff
            await _IApplicationUserRespository.UpdateStaffByAsync(profileVM.StaffProfileVM.ApplicationUser);

            ////- TO DO: Neu update khong thanh cong xoa anh vua add
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}
            return RedirectToAction(nameof(Profile));
            
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileVM profileVM)
        {
            ModelState.Clear();
            if (!TryValidateModel(profileVM.ChangePwdVM))
            {
                // view application user
                return View(profileVM);
            }
            //update password
            var user = await _userManager.GetUserAsync(User);
            //var user = await _userManager.FindByEmailAsync(profileVM.ChangePwdVM.Email);
            if (user == null)
            {
                return View(profileVM);
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, profileVM.ChangePwdVM.CurrentPwd, profileVM.ChangePwdVM.NewPwd);
            if (changePasswordResult.Succeeded)
            {
                // Xử lý thành công
                return RedirectToAction(nameof(Profile));
            }
            else
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction(nameof(Profile));

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
