using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Stationery")]
public partial class Stationery
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string? Material { get; set; }

    [StringLength(50)]
    public string? Origin { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Stationery")]
    public virtual Product Product { get; set; } = null!;
}
