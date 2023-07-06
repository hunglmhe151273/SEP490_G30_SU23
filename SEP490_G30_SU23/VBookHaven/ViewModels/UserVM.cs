using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.ViewModels
{
    public class UserVM
    {  
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Cần ghi đúng định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password là bắt buộc.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Mật khẩu phải có ít nhất một ký tự không chữ số và không phải là chữ cái, một chữ số và một chữ cái in hoa.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GenderList { get; set; }
        // Staff_ info

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        [StringLength(20, ErrorMessage = "Họ và Tên không được vượt quá 20 kí tự.")]
        public string? Staff_FullName { get; set; }
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime? Staff_Dob { get; set; }
        [Display(Name = "Số CMND")]
        [Required(ErrorMessage = "Số CMND là bắt buộc.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Số CMND phải có đúng 12 kí tự.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số CMND chỉ được chứa chữ số.")]
        public string? Staff_IdCard { get; set; }
        [Display(Name = "Địa chỉ nhân viên")]
        [Required(ErrorMessage = "Địa chỉ nhân viên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 kí tự.")]
        public string? Staff_Address { get; set; }
        [Display(Name = "Số điện thoại nhân viên")]
        [Required(ErrorMessage = "Số điện thoại nhân viên là bắt buộc.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
        public string? Staff_Phone { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Staff_ImageFile { get; set; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public bool? Staff_IsMale { get; set; }
        public String? RoleValidate { get; set; }
        public String? GenderValidate { get; set; }
        //Customer---------------------------------------------------------------------
        //[StringLength(20, ErrorMessage = "Họ và Tên không được vượt quá 20 kí tự.")]
        //[Display(Name = "Tên Tài Khoản")]
        //public String? Customer_UserName { get; set; }
        //[Display(Name = "Số điện thoại")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
        //public String? Customer_Phone { get; set; }
       
    }
}
