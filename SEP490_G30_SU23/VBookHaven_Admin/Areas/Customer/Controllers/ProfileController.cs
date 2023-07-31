using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using VBookHaven.DataAccess.Data;
using VBookHaven.DataAccess.Respository;
using VBookHaven.Models;
using VBookHaven.Models.DTO;
using VBookHaven.ViewModels;

namespace VBookHaven_Admin.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProfileController : ControllerBase
    {

        private readonly VBookHavenDBContext _dbContext;
        private readonly IApplicationUserRespository _applicationUserRespository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICustomerRespository _customerRespository;
        IMapper _mapper;
        public ProfileController(VBookHavenDBContext context, 
            IMapper mapper,
            IApplicationUserRespository applicationUserRespository,
            IWebHostEnvironment webHostEnvironment, ICustomerRespository customerRespository)
        {
            _applicationUserRespository = applicationUserRespository;
            _dbContext = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _customerRespository = customerRespository;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] IFormFile avatarFile, [FromForm] string customerDTOJson)
        {
            //to do JWT

            // Deserialize the JSON data to the Customer object
            var customerDTO = JsonSerializer.Deserialize<CustomerDTO>(customerDTOJson);
            VBookHaven.Models.Customer customer = _mapper.Map<CustomerDTO, VBookHaven.Models.Customer>(customerDTO);

            //if(applicationUser.Customer.CustomerId != customer.CustomerId)
            //{
            //    return BadRequest();
            //}

            //xu ly anh update, update duong dan anh cho customer
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (avatarFile != null)
            {
                string fileName = customer.CustomerId.ToString() + Path.GetExtension(avatarFile.FileName);
                string customerPath = Path.Combine(wwwRootPath, @"images\customer");
                if (!string.IsNullOrEmpty(customer.Image))
                {
                    //delete the old image
                    var oldImagePath =
                        Path.Combine(wwwRootPath, customer.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //add image, update url
                using (var fileStream = new FileStream(Path.Combine(customerPath, fileName), FileMode.Create))
                {
                    avatarFile.CopyTo(fileStream);
                }

                customer.Image = @"\images\customer\" + fileName;
            }
            await _customerRespository.UpdateCustomerProfile(customer);
            return Ok();
        }
        private async Task<ApplicationUser> getCustomerFromIdentity()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                return await _applicationUserRespository.GetCustomerByUIdAsync(userId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
