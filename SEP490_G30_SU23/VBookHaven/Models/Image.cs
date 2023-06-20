using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Image")]
public partial class Image
{
    [Key]
    public int ImageId { get; set; }

    [StringLength(50)]
    public string? ImageName { get; set; }

    public int? ProductId { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ImageCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("ImageEditors")]
    public virtual Account? Editor { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Images")]
    public virtual Product? Product { get; set; }
}
