using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Exchange
{
    public int ExchangeId { get; set; }

    public int? ProductId { get; set; }

    public int? BaseProductId { get; set; }

    public int? QuantityBaseProduct { get; set; }

    public virtual Product? BaseProduct { get; set; }

    public virtual Product? Product { get; set; }
}
