using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("SubCategory")]
public partial class SubCategory
{
    [Key]
    public int SubCategoryId { get; set; }

    [StringLength(50)]
    public string? SubCategoryName { get; set; }

    public int? CategoryId { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("SubCategories")]
    public virtual Category? Category { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("SubCategoryCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("SubCategoryEditors")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("SubCategory")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
