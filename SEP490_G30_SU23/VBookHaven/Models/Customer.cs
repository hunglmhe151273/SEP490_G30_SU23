using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Customer")]
public partial class Customer
{
    [Key]
    public int AccountId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Customer")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<ShippingInfo> ShippingInfos { get; set; } = new List<ShippingInfo>();
}
