using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public string? ImageName { get; set; }

    public int? ProductId { get; set; }

    public bool? Status { get; set; }

    public virtual Product? Product { get; set; }
}
