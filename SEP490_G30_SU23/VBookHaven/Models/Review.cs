using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    public int ReviewId { get; set; }

    public double? Rating { get; set; }

    [Column(TypeName = "text")]
    public string? Comment { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("ReviewAccounts")]
    public virtual Account? Account { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ReviewCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("ReviewEditors")]
    public virtual Account? Editor { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Reviews")]
    public virtual Product? Product { get; set; }
}
