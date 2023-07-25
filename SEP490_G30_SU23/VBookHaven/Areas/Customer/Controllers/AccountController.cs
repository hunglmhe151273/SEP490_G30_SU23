using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Security.Cryptography;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.ViewModels;
using VBookHaven.ViewModels;

namespace VBookHaven.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly IShippingInfoRepository _shippingInfoRepository;
        private readonly ICustomerRespository _customerRespository;
        private readonly IApplicationUserRespository _applicationUserRespository;
        public AccountController(IShippingInfoRepository shippingInfoRespository,
            IApplicationUserRespository applicationUserRespository,
            ICustomerRespository customerRespository)
        {
            _shippingInfoRepository = shippingInfoRespository;
            _applicationUserRespository = applicationUserRespository;
            _customerRespository = customerRespository;
        }
        public async Task<IActionResult> ShipInfo()
        {
            try
            {
                //getCustomerIDFromIdentity
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
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
                //getCustomerIDFromIdentity
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
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
     
    }
}
