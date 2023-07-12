using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get customer by uid
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
                int cid = applicationUser.Customer.CustomerId;
                var shippingInfos = await _shippingInfoRepository.GetAllShipInfoByUIDAsync(cid);
                //view application user
                return View(shippingInfos);
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get customer by uid
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
                int cid = applicationUser.Customer.CustomerId;
                ShippingInfoVM model = new ShippingInfoVM();
                model.CustomerId = cid;
                //view application user
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                //get customer by uid
                ApplicationUser applicationUser = await _applicationUserRespository.GetCustomerByUIdAsync(userId);
                int cid = applicationUser.Customer.CustomerId;
                ShippingInfoVM model = new ShippingInfoVM();
                //lấy thông tin lên form theo shipinfoId và customerID
                model.ShippingInfo = await _shippingInfoRepository.GetShipInfoByIdAsync(cid, shipInfoId);
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
    }
}
