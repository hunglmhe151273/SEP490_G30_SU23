using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Address { get; set; }

    public string? SupplierName { get; set; }

    public string? Phone { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
