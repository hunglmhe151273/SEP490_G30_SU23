using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public DateTime? Date { get; set; }

    public int? StaffId { get; set; }

    public int? SupplierId { get; set; }

    public String? Status { get; set; }
    public String? Description { get; set; }
    public Double? AmountPaid { get; set; }
    public double ? VAT { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Staff? Staff { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
