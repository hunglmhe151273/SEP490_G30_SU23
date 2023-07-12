using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Status { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    //public int? ShippingInfoId { get; set; }

    //public virtual ShippingInfo? ShippingInfo { get; set; }

    //new
    public string? ShippingInfo { get; set; }
}
