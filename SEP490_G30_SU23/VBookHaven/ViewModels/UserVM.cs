using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.ViewModels
{
    public class UserVM
    {  
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Passwords must have at least one non-alphanumeric character, one digit, and one uppercase letter.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GenderList { get; set; }
        // Staff_ info

        [Display(Name = "Họ và Tên")]
        [StringLength(20, ErrorMessage = "Họ và Tên không được vượt quá 20 kí tự.")]
        public string? Staff_FullName { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? Staff_Dob { get; set; }
        [Display(Name = "Số CMND")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Số CMND phải có đúng 12 kí tự.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số CMND chỉ được chứa chữ số.")]
        public string? Staff_IdCard { get; set; }
        [Display(Name = "Địa chỉ nhân viên")]
        [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 kí tự.")]
        public string? Staff_Address { get; set; }
        [Display(Name = "Số điện thoại nhân viên")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
        public string? Staff_Phone { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Staff_ImageFile { get; set; }
        [Display(Name = "Giới tính")]
        public bool? Staff_IsMale { get; set; }
        //Customer
        [StringLength(20, ErrorMessage = "Họ và Tên không được vượt quá 20 kí tự.")]
        [Display(Name = "Tên Tài Khoản")]
        public String? Customer_UserName { get; set; }
        [Display(Name = "Số điện thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
        public String? Customer_Phone { get; set; }
    }
}
