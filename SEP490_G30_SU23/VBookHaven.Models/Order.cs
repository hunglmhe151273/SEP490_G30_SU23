﻿using System;
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

    //// new
    //public string? ShippingInfo { get; set; }

    public string? CustomerName { get; set; }

    public string? ShipAddress { get; set; }

    public string? Phone { get; set; }

    // new 2

    public string? Note { get; set; }

    public int? AmountPaid { get; set; }
    
    public double? VAT { get; set; }

    public int? StaffId { get; set; }

    public virtual Staff? Staff { get; set; }

	public virtual ICollection<OrderPaymentHistory> OrderPaymentHistories { get; set; } = new List<OrderPaymentHistory>();
}
