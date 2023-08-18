﻿using Microsoft.AspNetCore.Identity.UI.Services;
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
using VBookHaven.DataAccess.Respository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using VBookHaven.DataAccess.Data;
using AutoMapper;

namespace VBookHaven_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
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
        private readonly VBookHavenDBContext _dbContext;
        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            //ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IApplicationUserRespository applicationUserRespository,
            IWebHostEnvironment webHostEnvironment,
            VBookHavenDBContext dbContext)
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
            _dbContext = dbContext;
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_Owner)]
        public IActionResult Create()
        {
            UserVM userVM = new()
            {
                RoleList = GetRoleList(),
            };
            return View(userVM);
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> Create(UserVM model, string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //if (String.IsNullOrEmpty(model.Role))
            //{
            //    model.RoleValidate = "Hãy chọn vị trí của nhân viên";
            //}
            model.Role = SD.Role_Staff;
            if (model.Staff_IsMale == null)
            {
                model.GenderValidate = "Hãy chọn giới tính của nhân viên";
            }
            if (model.Staff_IsMale == null || String.IsNullOrEmpty(model.Role))
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
                        $"Bạn được chủ cửa hàng Bookstore Management System tạo tài khoản với vị trí: {model.Role}. Mật khẩu tạm thời của bạn là: {model.Password}. Để kích hoạt tài khoản bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click vào đây</a>.");
                    TempData["success"] = "Thêm nhân viên thành công";
                    return RedirectToAction("Index", "User");
                }

                //- TO DO: Neu add khong thanh cong xoa anh vua add
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        // Customize the error message for duplicate username
                        error.Description = "Email đã tồn tại. Hãy nhập email khác.";
                        TempData["error"] = "Thêm nhân viên thất bại";
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            model.RoleList = GetRoleList();
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get application user by id
                ProfileVM profileVM = new ProfileVM();
                profileVM.StaffProfileVM.ApplicationUser = await _IApplicationUserRespository.GetStaffByUIdAsync(userId);//lấy ra các thông tin liên quan đến user bằng userID(Application là bảng User)
                if (profileVM.StaffProfileVM.ApplicationUser.Staff == null) return NotFound();
                //view application user
                return View(profileVM);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
      
        [HttpPost]
        [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
        public async Task<IActionResult> Profile(ProfileVM profileVM)
        {
            //To do: Validate user and model?
            if (profileVM.StaffProfileVM.ApplicationUser.Staff.IsMale == null)
            {
                profileVM.StaffProfileVM.GenderValidate = "Hãy chọn giới tính của nhân viên";
                return View(profileVM);
            }
            ModelState.Clear();
            if (!TryValidateModel(profileVM.StaffProfileVM))
            {
                TempData["error"] = "Cập nhật hồ sơ thất bại";
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
            TempData["success"] = "Cập nhật hồ sơ thành công";
            ////- TO DO: Neu update khong thanh cong xoa anh vua add
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}
            return RedirectToAction(nameof(Profile));
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Owner + "," + SD.Role_Staff)]
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
                TempData["success"] = "Thay đổi mật khẩu thành công";
                return RedirectToAction(nameof(Profile));
            }
            else
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            TempData["error"] = "Thay đổi mật khẩu thất bại";
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
            return _roleManager.Roles.Where(x => x.Name != SD.Role_Customer && x.Name != SD.Role_Owner).Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
        }
        private IEnumerable<SelectListItem> GetRoleListToUpdateStaff(String selectedRole)
        {
            var roles = new List<SelectListItem>();

            roles.AddRange(_roleManager.Roles.Where(x => x.Name != SD.Role_Customer && x.Name != SD.Role_Owner).Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            }));

            // Set the selected role as the default option
            foreach (var role in roles)
            {
                if (role.Value == selectedRole)
                {
                    role.Selected = true;
                    break;
                }
            }
            return roles;
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> Edit(String userId)
        {
            UpdateStaffVM VM = new UpdateStaffVM();
            VM.ApplicationUser = await _IApplicationUserRespository.GetStaffByUIdAsync(userId);//lấy ra các thông tin liên quan đến user bằng userID(Application là bảng User)
            if (VM.ApplicationUser.Staff == null) return NotFound();
            var user = await _userManager.FindByIdAsync(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);
            VM.RoleList = GetRoleListToUpdateStaff(currentRoles.FirstOrDefault());
            //view application user
            return View(VM);
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> Edit(UpdateStaffVM model, string? returnUrl)
        {

            returnUrl ??= Url.Content("~/");
            //if (String.IsNullOrEmpty(model.Role))
            //{
            //    model.RoleList = GetRoleListToUpdateStaff(model.Role);
            //    ModelState.AddModelError("Role", "Hãy chọn vị trí của nhân viên");
            //}
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //update role
            //await updateRoleByUIDAsync(model.ApplicationUser.Id, model.Role);

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (model.Staff_ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Staff_ImageFile.FileName);
                string staffPath = Path.Combine(wwwRootPath, @"images\staff");
                if (!string.IsNullOrEmpty(model.ApplicationUser.Staff.Image))
                {
                    //delete the old image
                    var oldImagePath =
                        Path.Combine(wwwRootPath, model.ApplicationUser.Staff.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //add image, update url
                using (var fileStream = new FileStream(Path.Combine(staffPath, fileName), FileMode.Create))
                {
                    model.Staff_ImageFile.CopyTo(fileStream);
                }

                model.ApplicationUser.Staff.Image = @"\images\staff\" + fileName;
            }

            //update staff
            await _IApplicationUserRespository.UpdateStaffByAsync(model.ApplicationUser);
            TempData["success"] = "Cập nhật hồ sơ nhân viên thành công";
            ////- TO DO: Neu update khong thanh cong xoa anh vua add
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}
            return RedirectToAction(nameof(Index));
        }
        private async Task updateRoleByUIDAsync(string uid, string newRole)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
            {
                // Handle the case where the user is not found
                return;
            }
            // Get the user's current roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            // Remove the user from their current roles
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            // Add the user to the new role
            await _userManager.AddToRoleAsync(user, newRole);
        }
        #region API CALLS
        [HttpGet]
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> GetAllStaff()
        {
            //respository
            List<ApplicationUser> objUserList = await _IApplicationUserRespository.GetAllStaffAsync();
            foreach (var user in objUserList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            }
            objUserList = objUserList.Where(x => x.Role != SD.Role_Owner).ToList();
            return Json(new { data = objUserList });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Owner)]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {

            var objFromDb = await _dbContext.ApplicationUsers.SingleOrDefaultAsync(a => a.Id.Equals(id));
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi cập nhật" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked and we need to unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _dbContext.SaveChanges();
            return Json(new { success = true, message = "Cập nhật thành công" });
        }

        #endregion

    }
}
