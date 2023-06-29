using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Book
{
    public int ProductId { get; set; }

    public int? Pages { get; set; }

    public string? Language { get; set; }

    public string? Publisher { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
