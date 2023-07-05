using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VBookHaven.Models;

namespace VBookHaven.ViewModels
{
    public class StaffProfileVM
    {  
       
        [EmailAddress(ErrorMessage = "Cần ghi đúng định dạng Email")]
        [Display(Name = "Email")]
        [ValidateNever]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password là bắt buộc.")]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Passwords must have at least one non-alphanumeric character, one digit, and one uppercase letter.")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        // Staff_ info
        public ApplicationUser? ApplicationUser { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Staff_ImageFile { get; set; }
        //
        public String? RoleValidate { get; set; }
        public String? GenderValidate { get; set; }
       
    }
}
