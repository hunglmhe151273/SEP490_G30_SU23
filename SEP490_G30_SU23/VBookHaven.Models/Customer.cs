using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? UserName { get; set; }

    public string? Phone { get; set; }

    public string? AccountId { get; set; }
    public virtual ApplicationUser? Account { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ShippingInfo> ShippingInfos { get; set; } = new List<ShippingInfo>();
}
