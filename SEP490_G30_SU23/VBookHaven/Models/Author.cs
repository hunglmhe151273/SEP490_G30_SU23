using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

[Table("Author")]
public partial class Author
{
    [Key]
    public int AuthorId { get; set; }

    [StringLength(50)]
    public string? AuthorName { get; set; }

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    public bool? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public int? EditorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("AuthorCreators")]
    public virtual Account? Creator { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("AuthorEditors")]
    public virtual Account? Editor { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Authors")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
