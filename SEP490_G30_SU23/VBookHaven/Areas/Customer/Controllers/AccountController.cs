using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.Models.ViewModels;
using VBookHaven.ViewModels;

namespace VBookHaven.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly HttpClient client = null;
        private string profileApiUrl = "";
        private readonly IShippingInfoRepository _shippingInfoRepository;
        private readonly ICustomerRespository _customerRespository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IApplicationUserRespository _applicationUserRespository;
        IMapper _mapper;
        public AccountController(IShippingInfoRepository shippingInfoRespository,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IApplicationUserRespository applicationUserRespository,
            ICustomerRespository customerRespository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _shippingInfoRepository = shippingInfoRespository;
            _applicationUserRespository = applicationUserRespository;
            _customerRespository = customerRespository;
            client = new HttpClient();
            profileApiUrl = "https://localhost:7123/customer/profile/update";
        }
        public async Task<IActionResult> ShipInfo()
        {
            try
            {
                //getCustomerIDFromIdentity
                ApplicationUser applicationUser = await getCustomerFromIdentity();
                if (applicationUser == null) { return NotFound(); }
                int cid = applicationUser.Customer.CustomerId;

                var shippingInfos = await _shippingInfoRepository.GetAllShipInfoByCusIDAsync(cid);
                ShippingInfoVM model = new ShippingInfoVM();
                model.ShippingInfos = shippingInfos;
                //view application user
                return View(model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateShipInfo()
        {
            try
            {
                //getCustomerIDFromIdentity
                ApplicationUser applicationUser = await getCustomerFromIdentity();
                if (applicationUser == null) { return NotFound(); }
                int cid = applicationUser.Customer.CustomerId;

                ShippingInfoVM model = new ShippingInfoVM();
                model.CustomerId = cid;// lên view sau đó đẩy post sẽ biết customer nào
                //neu khong có địa chỉ nào. Mặc định là default là true
                 var shippingInfos = await _shippingInfoRepository.GetAllShipInfoByCusIDAsync(cid);
                if(shippingInfos.Count() == 0)
                {
                    model.IsDefault = true;
                    model.shippingInfosIsNull = true;
                }
                //lay dia chi theo cid
                return View(model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditShipInfo(int shipInfoId)
        {
            try
            {
                ApplicationUser applicationUser = await getCustomerFromIdentity();
                if (applicationUser == null) { return NotFound(); }
                int cid = applicationUser.Customer.CustomerId;

                ShippingInfoVM model = new ShippingInfoVM();
                //lấy thông tin lên form theo shipinfoId và customerID
                model.ShippingInfo = await _shippingInfoRepository.GetShipInfoByCusIdAndShipInfoIdAsync(cid, shipInfoId);
                //set IsDefault hay không
                if(applicationUser.Customer.DefaultShippingInfoId == model.ShippingInfo.ShipInfoId)
                {
                    model.IsDefault = true;
                }
                model.CustomerId = cid;
                //view application user
                return View(model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditShipInfo(ShippingInfoVM shippingInfoVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(shippingInfoVM);
            }
            // update shipinfo
            await _shippingInfoRepository.UpdateShipInfoAsync(shippingInfoVM.ShippingInfo);

            if (shippingInfoVM.IsDefault)
            {
                //update customer default address id
                await _customerRespository.UpdateCustomerDefaultShipInfoAsync(shippingInfoVM.CustomerId, shippingInfoVM.ShippingInfo.ShipInfoId);
            }

            return LocalRedirect(returnUrl);
        }
        [HttpPost]
        public async Task<IActionResult> CreateShipInfo(ShippingInfoVM shippingInfoVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                //neu khong có địa chỉ nào. Mặc định là default là true
                var shippingInfos = await _shippingInfoRepository.GetAllShipInfoByCusIDAsync(shippingInfoVM.CustomerId);
                if (shippingInfos.Count() == 0)
                {
                    shippingInfoVM.IsDefault = true;
                    shippingInfoVM.shippingInfosIsNull = true;
                }
                return View(shippingInfoVM);
            }
           
            if (shippingInfoVM.IsDefault)
            {
                //create new and update cutomer default address id
                await _customerRespository.UpdateCustomerDefaultShipInfoOnCreateAsync(shippingInfoVM);
            }
            else
            {
                shippingInfoVM.ShippingInfo.CustomerId = shippingInfoVM.CustomerId;
                // add shipinfo that have customer id
                await _shippingInfoRepository.AddShippingInfoAsync(shippingInfoVM.ShippingInfo);
            }

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteShipInfo(int shipInfoId)
        {
            try
            {
                //shippid đấy là của customer đấy  --> tìm theo id, nếu customer == cus identity thì xóa
                //getCustomerIDFromIdentity
                ApplicationUser applicationUser = await getCustomerFromIdentity();
                if (applicationUser == null) { return NotFound(); }
                int cid = applicationUser.Customer.CustomerId;

                var shippingInfo = await _shippingInfoRepository.GetShipInfoByCusIdAndShipInfoIdAsync(cid, shipInfoId);
                if(shippingInfo != null && shippingInfo.Customers.Count == 0)
                {
                    //kiểm tra shipinfo đấy có nhiều list cus hay không
                    await _shippingInfoRepository.DeleteShipInfoAsync(shippingInfo);
                }

                //lay dia chi theo cid
                return Redirect(nameof(ShipInfo));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                ApplicationUser applicationUser = await getCustomerFromIdentity();
                if (applicationUser == null) { return NotFound(); }
                int cid = applicationUser.Customer.CustomerId;

                CustomerProfileVM c = new CustomerProfileVM();
                c.Customer = applicationUser.Customer;
                c.Email = applicationUser.Email;
                //view application user
                return View(c);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerProfileVM profileVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(profileVM);
            }
            //Call updateCustomer profile
            CustomerDTO customerDTO =  _mapper.Map<VBookHaven.Models.Customer, CustomerDTO>(profileVM.Customer);

            // Serialize the customer object to JSON
            var customerDTOJson = JsonSerializer.Serialize(customerDTO, new JsonSerializerOptions
            {
                // Use this option to include null values in the JSON
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
            });
            //serialize customer, form gui vao multiple file content
            var content = new MultipartFormDataContent();
            if (profileVM.Customer_ImageFile != null)
            {
                using var stream = profileVM.Customer_ImageFile.OpenReadStream();
                var streamContent = new StreamContent(stream);
                var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
                content.Add(fileContent, "avatarFile", profileVM.Customer_ImageFile.FileName);
            }
            var customerContent = new StringContent(customerDTOJson, Encoding.UTF8, "application/json");
            content.Add(customerContent, "customerDTOJson");
            var postTask = await client.PostAsync(profileApiUrl, content);
            if (!postTask.IsSuccessStatusCode)
            {
                if (postTask.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("ErrorCode", "Home", new { statusCodes = postTask.StatusCode });
                }
            }
            return RedirectToAction(nameof(Edit));
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePwdVM changePwdVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                // view application user
                return View(changePwdVM);
            }
            //update password
            try
            {
                var user = await _userManager.GetUserAsync(User);
                //var user = await _userManager.FindByEmailAsync(profileVM.ChangePwdVM.Email);
                if (user == null)
                {
                    return View(changePwdVM);
                }
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePwdVM.CurrentPwd, changePwdVM.NewPwd);
                if (changePasswordResult.Succeeded)
                {
                    // Xử lý thành công
                    TempData["success"] = "Thay đổi mật khẩu thành công";
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                return View(changePwdVM);
            }

        }
        private async Task<ApplicationUser> getCustomerFromIdentity()
        {
            try {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                return await _applicationUserRespository.GetCustomerByUIdAsync(userId);
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
