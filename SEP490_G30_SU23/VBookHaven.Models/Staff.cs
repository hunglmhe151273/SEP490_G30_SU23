using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    [Display(Name = "Họ và Tên")]
    [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
    [StringLength(20, ErrorMessage = "Họ và Tên không được vượt quá 20 kí tự.")]
    public string? FullName { get; set; }
    [Display(Name = "Ngày sinh")]
    [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]

    public DateTime? Dob { get; set; }
    [Display(Name = "Số CMND")]
    [Required(ErrorMessage = "Số CMND là bắt buộc.")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "Số CMND phải có đúng 12 kí tự.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Số CMND chỉ được chứa chữ số.")]
    public string? IdCard { get; set; }
    [Display(Name = "Địa chỉ nhân viên")]
    [Required(ErrorMessage = "Địa chỉ nhân viên là bắt buộc.")]
    [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 kí tự.")]
    public string? Address { get; set; }
    [Display(Name = "Số điện thoại nhân viên")]
    [Required(ErrorMessage = "Số điện thoại nhân viên là bắt buộc.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
    public string? Phone { get; set; }
    public string? Image { get; set; }
    [Display(Name = "Giới tính")]
    [Required(ErrorMessage = "Giới tính là bắt buộc.")]
    public bool? IsMale { get; set; }

    public string? AccountId { get; set; }
    public virtual ApplicationUser? Account { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public virtual ICollection<PurchasePaymentHistory> PurchasePaymentHistories { get; set; } = new List<PurchasePaymentHistory>();

}
