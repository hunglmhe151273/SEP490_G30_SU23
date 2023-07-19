using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBookHaven.Models;

public partial class SubCategory
{
    public int SubCategoryId { get; set; }
    [Required(ErrorMessage = "Tên không được để trống.")]
    [StringLength(30, ErrorMessage = "Độ dài không vượt quá 30 kí tự.")]
    public string? SubCategoryName { get; set; }

    public int? CategoryId { get; set; }

    public bool Status { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
