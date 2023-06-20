using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Book")]
public partial class Book
{
    [Key]
    public int ProductId { get; set; }

    public int? Pages { get; set; }

    [StringLength(50)]
    public string? Language { get; set; }

    [StringLength(100)]
    public string? Publisher { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Book")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
