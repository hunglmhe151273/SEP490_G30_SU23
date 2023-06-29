using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Product")]
[Index("Barcode", Name = "UK_Product", IsUnique = true)]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Barcode { get; set; }

    [StringLength(50)]
    public string? QuantityPerUnit { get; set; }

    public int? UnitInStock { get; set; }

    public int? PurchasePrice { get; set; }

    public int? RetailPrice { get; set; }

    public double? RetailDiscount { get; set; }

    public int? WholesalePrice { get; set; }

    public double? WholesaleDiscount { get; set; }

    [StringLength(50)]
    public string? Size { get; set; }

    [StringLength(50)]
    public string? Weight { get; set; }

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    public bool? Status { get; set; }

    public bool? IsBook { get; set; }

    public int? SubCategoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [InverseProperty("Product")]
    public virtual Book? Book { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ProductCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("ProductEditors")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Product")]
    public virtual Stationery? Stationery { get; set; }

    [ForeignKey("SubCategoryId")]
    [InverseProperty("Products")]
    public virtual SubCategory? SubCategory { get; set; }
}
