using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Stationery
{
    public int ProductId { get; set; }

    public string? Material { get; set; }

    public string? Origin { get; set; }

    public string? Brand { get; set; }

    public virtual Product Product { get; set; } = null!;
}
