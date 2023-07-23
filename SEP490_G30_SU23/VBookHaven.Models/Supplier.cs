using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }
    [Display(Name = "Địa chỉ chi tiết")]
    [Required(ErrorMessage = "Địa chỉ chi tiết là bắt buộc")]
    [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 kí tự")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
    [Display(Name = "Tên nhà cung cấp")]
    [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được vượt quá 100 kí tự")]
    public string? SupplierName { get; set; }
    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [Display(Name = "Số điện thoại")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 kí tự.")]
    public string? Phone { get; set; }

    public bool Status { get; set; }
    [Display(Name = "Ghi chú")]
    [StringLength(1000, ErrorMessage = "Ghi chú không được vượt quá 1000 kí tự")]
    public string? Description { get; set; }
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    [StringLength(100, ErrorMessage = "Email không được vượt quá 100 kí tự")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Tỉnh/thành là bắt buộc")]
    public int? ProvinceCode { get; set; }
    public string? Province { get; set; }
    [Required(ErrorMessage = "Quận/Huyện là bắt buộc")]
    public int? DistrictCode { get; set; }
    public string? District { get; set; }
    [Required(ErrorMessage = "Phường xã là bắt buộc")]
    public int? WardCode { get; set; }
    public string? Ward { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
