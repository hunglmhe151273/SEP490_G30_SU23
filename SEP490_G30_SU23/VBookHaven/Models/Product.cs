using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Barcode { get; set; }

    public string? Unit { get; set; }

    public int? UnitInStock { get; set; }

    public int? PurchasePrice { get; set; }

    public int? RetailPrice { get; set; }

    public double? RetailDiscount { get; set; }

    public int? WholesalePrice { get; set; }

    public double? WholesaleDiscount { get; set; }

    public string? Size { get; set; }

    public string? Weight { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public bool? IsBook { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual ICollection<Exchange> ExchangeBaseProducts { get; set; } = new List<Exchange>();

    public virtual ICollection<Exchange> ExchangeProducts { get; set; } = new List<Exchange>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Stationery? Stationery { get; set; }

    public virtual SubCategory? SubCategory { get; set; }
}
