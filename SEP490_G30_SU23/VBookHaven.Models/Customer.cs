using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class Customer
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
    public DateTime? DOB {  get; set; }
    [Display(Name = "Giới tính")]
    public bool? IsMale { get; set; }
    [Display(Name = "Nhóm khách hàng")]
    public bool IsWholesale { get; set; }
    public string? Image { get; set; }
    public bool? Status { get; set; }
    [Display(Name = "Nhóm Khách Hàng")]
    public string? AccountId { get; set; }
    public virtual ApplicationUser? Account { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ShippingInfo> ShippingInfos { get; set; } = new List<ShippingInfo>();
    //new
    public int? DefaultShippingInfoId { get; set; }

    public virtual ShippingInfo? DefaultShippingInfo { get; set; }
}
