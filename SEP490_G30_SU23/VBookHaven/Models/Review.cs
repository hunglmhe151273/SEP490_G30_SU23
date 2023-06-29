using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public double? Rating { get; set; }

    public string? Comment { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    public virtual Product? Product { get; set; }
}
