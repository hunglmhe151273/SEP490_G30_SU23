using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.ViewModels;

namespace VBookHaven.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly IShippingInfoRespository _shippingInfoRespository;
        private readonly IApplicationUserRespository _applicationUserRespository;
        public AccountController(IShippingInfoRespository shippingInfoRespository, IApplicationUserRespository applicationUserRespository)
        {
            _shippingInfoRespository = shippingInfoRespository;
            _applicationUserRespository = applicationUserRespository;
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
                var shippingInfos = await _shippingInfoRespository.GetAllShipInfoByUIDAsync(cid);
                //view application user
                return View(shippingInfos);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
