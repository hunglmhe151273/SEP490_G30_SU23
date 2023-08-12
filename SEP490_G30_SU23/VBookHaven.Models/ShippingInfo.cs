using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class ShippingInfo
{
    [ValidateNever]
    public int ShipInfoId { get; set; }
    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
    public string? Phone { get; set; }
    [Display(Name = "Địa chỉ chi tiết  *")]
    [Required(ErrorMessage = "Địa chỉ chi tiết là bắt buộc")]
    [StringLength(100, ErrorMessage = "Địa chỉ chi tiết không được vượt quá 100 kí tự.")]
    public string? ShipAddress { get; set; }
    public int? CustomerId { get; set; }
    public bool? Status { get; set; }
    [Display(Name = "Tên người nhận ")]
    [Required(ErrorMessage = "Tên người nhận là bắt buộc")]
    [StringLength(50, ErrorMessage = "Tên người nhận không được vượt quá 50 kí tự.")]
    public string? CustomerName { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    //Address ----------------------------------------------------------------------
    [Required(ErrorMessage = "Tỉnh/thành là bắt buộc")]
    public int? ProvinceCode { get; set; }
    public string? Province { get; set; }
    [Required(ErrorMessage = "Quận/Huyện là bắt buộc")]
    public int? DistrictCode { get; set; }
    public string? District { get; set; }
    [Required(ErrorMessage = "Phường xã là bắt buộc")]
    public int? WardCode { get; set; }
    public string? Ward { get; set; }
    //Address -----------------------------------------------------------------------

}
