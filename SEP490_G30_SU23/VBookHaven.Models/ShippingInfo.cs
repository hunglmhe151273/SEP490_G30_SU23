using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class ShippingInfo
{
    public int ShipInfoId { get; set; }

    public string? Phone { get; set; }

    public string? ShipAddress { get; set; }

    public int? CustomerId { get; set; }

    public bool? Status { get; set; }

    public string? CustomerName { get; set; }

    public virtual Customer? Customer { get; set; }

    //new
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
