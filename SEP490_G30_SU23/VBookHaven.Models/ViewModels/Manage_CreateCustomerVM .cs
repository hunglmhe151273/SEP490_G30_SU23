using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VBookHaven.Models;

namespace VBookHaven.ViewModels
{
    public class Manage_CreateCustomerVM
    {
        public Manage_CreateCustomerVM() {
            Customer = new Customer();
        }
        public Customer Customer { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Customer_ImageFile { get; set; }
    }
}
