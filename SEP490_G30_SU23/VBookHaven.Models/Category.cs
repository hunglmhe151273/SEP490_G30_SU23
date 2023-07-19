using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    [Display(Name = "Tên loại sản phẩm")]
    public string? CategoryName { get; set; }
    [Display(Name = "Trạng thái")]
    public bool Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
