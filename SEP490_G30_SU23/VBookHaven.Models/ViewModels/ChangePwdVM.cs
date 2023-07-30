using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.ViewModels
{
    public class ChangePwdVM
    {
        //[ValidateNever]
        //public String Email { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu hiện tại")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string CurrentPwd { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        [StringLength(20, ErrorMessage = "{0} phải có ít nhất {2} và tối đa {1} ký tự. ", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Mật khẩu phải có ít nhất một ký tự không thuộc bảng chữ cái, một chữ số và một chữ cái in hoa.")]
        public string NewPwd { get; set; }

        [Required(ErrorMessage = "Hãy xác nhận lại mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPwd", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu mới không khớp.")]
        public string ConfirmNewPwd { get; set; }
    }
}
