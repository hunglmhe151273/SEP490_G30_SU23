using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? AuthorName { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
