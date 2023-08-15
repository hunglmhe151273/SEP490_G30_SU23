using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    [Display(Name = "Tên loại sản phẩm")]
    [Required(ErrorMessage = "Tên không được để trống.")]
    [StringLength(30, ErrorMessage = "Độ dài không vượt quá 30 kí tự.")]
    public string? CategoryName { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
