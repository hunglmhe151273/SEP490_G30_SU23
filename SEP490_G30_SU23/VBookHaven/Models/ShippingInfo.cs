using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("ShippingInfo")]
public partial class ShippingInfo
{
    [Key]
    public int ShipInfoId { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? ShipAddress { get; set; }

    public int? CustomerId { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ShippingInfoCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ShippingInfos")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("ShippingInfoEditors")]
    public virtual Account? Editor { get; set; }

    [InverseProperty("ShippingInfo")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
