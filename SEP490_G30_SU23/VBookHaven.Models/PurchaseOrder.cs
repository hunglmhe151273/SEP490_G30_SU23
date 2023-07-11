using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public DateTime? Date { get; set; }

    public int? StaffId { get; set; }

    public int? SupplierId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Staff? Staff { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
