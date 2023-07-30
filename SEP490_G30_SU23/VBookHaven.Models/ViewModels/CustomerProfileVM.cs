using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VBookHaven.Models;

namespace VBookHaven.ViewModels
{
    public class CustomerProfileVM
    {

        [EmailAddress(ErrorMessage = "Cần ghi đúng định dạng Email")]
        [Display(Name = "Email")]
        [ValidateNever]
        public string Email { get; set; }
      
        public Customer Customer { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Customer_ImageFile { get; set; }

    }
}
