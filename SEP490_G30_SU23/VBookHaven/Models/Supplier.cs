using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    public int SupplierId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? SupplierName { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("SupplierCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("SupplierEditors")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
