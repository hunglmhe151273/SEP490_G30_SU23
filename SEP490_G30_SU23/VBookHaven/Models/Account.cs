using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    [StringLength(50)]
    public string? Username { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    [Column("RoleID")]
    public int? RoleId { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<Author> AuthorCreators { get; set; } = new List<Author>();

    [InverseProperty("Editor")]
    public virtual ICollection<Author> AuthorEditors { get; set; } = new List<Author>();

    [InverseProperty("Creator")]
    public virtual ICollection<Category> CategoryCreators { get; set; } = new List<Category>();

    [InverseProperty("Editor")]
    public virtual ICollection<Category> CategoryEditors { get; set; } = new List<Category>();

    [ForeignKey("CreatorId")]
    [InverseProperty("InverseCreator")]
    public virtual Account? Creator { get; set; }

    [InverseProperty("Account")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("InverseEditor")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<Image> ImageCreators { get; set; } = new List<Image>();

    [InverseProperty("Editor")]
    public virtual ICollection<Image> ImageEditors { get; set; } = new List<Image>();

    [InverseProperty("Creator")]
    public virtual ICollection<Account> InverseCreator { get; set; } = new List<Account>();

    [InverseProperty("Editor")]
    public virtual ICollection<Account> InverseEditor { get; set; } = new List<Account>();

    [InverseProperty("Creator")]
    public virtual ICollection<Order> OrderCreators { get; set; } = new List<Order>();

    [InverseProperty("Editor")]
    public virtual ICollection<Order> OrderEditors { get; set; } = new List<Order>();

    [InverseProperty("Creator")]
    public virtual ICollection<Product> ProductCreators { get; set; } = new List<Product>();

    [InverseProperty("Editor")]
    public virtual ICollection<Product> ProductEditors { get; set; } = new List<Product>();

    [InverseProperty("Creator")]
    public virtual ICollection<PurchaseOrder> PurchaseOrderCreators { get; set; } = new List<PurchaseOrder>();

    [InverseProperty("Editor")]
    public virtual ICollection<PurchaseOrder> PurchaseOrderEditors { get; set; } = new List<PurchaseOrder>();

    [InverseProperty("Account")]
    public virtual ICollection<Review> ReviewAccounts { get; set; } = new List<Review>();

    [InverseProperty("Creator")]
    public virtual ICollection<Review> ReviewCreators { get; set; } = new List<Review>();

    [InverseProperty("Editor")]
    public virtual ICollection<Review> ReviewEditors { get; set; } = new List<Review>();

    [ForeignKey("RoleId")]
    [InverseProperty("Accounts")]
    public virtual Role? Role { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<Role> RoleCreators { get; set; } = new List<Role>();

    [InverseProperty("Editor")]
    public virtual ICollection<Role> RoleEditors { get; set; } = new List<Role>();

    [InverseProperty("Creator")]
    public virtual ICollection<ShippingInfo> ShippingInfoCreators { get; set; } = new List<ShippingInfo>();

    [InverseProperty("Editor")]
    public virtual ICollection<ShippingInfo> ShippingInfoEditors { get; set; } = new List<ShippingInfo>();

    [InverseProperty("Account")]
    public virtual Staff? Staff { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<SubCategory> SubCategoryCreators { get; set; } = new List<SubCategory>();

    [InverseProperty("Editor")]
    public virtual ICollection<SubCategory> SubCategoryEditors { get; set; } = new List<SubCategory>();

    [InverseProperty("Creator")]
    public virtual ICollection<Supplier> SupplierCreators { get; set; } = new List<Supplier>();

    [InverseProperty("Editor")]
    public virtual ICollection<Supplier> SupplierEditors { get; set; } = new List<Supplier>();
}
