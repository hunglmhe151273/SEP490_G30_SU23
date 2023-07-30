using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Họ và Tên không được vượt quá 50 kí tự.")]
        public string? FullName { get; set; }
        [Display(Name = "Số điện thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
        public string? Phone { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? DOB { get; set; }
        [Display(Name = "Giới tính")]
        public bool? IsMale { get; set; }
        public string? Image { get; set; }
    }
}
