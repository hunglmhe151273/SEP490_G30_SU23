using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    public int? ShippingInfoId { get; set; }

    public int? CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("OrderCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("OrderEditors")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("ShippingInfoId")]
    [InverseProperty("Orders")]
    public virtual ShippingInfo? ShippingInfo { get; set; }
}
