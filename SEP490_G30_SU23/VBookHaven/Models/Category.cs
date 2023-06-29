using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
