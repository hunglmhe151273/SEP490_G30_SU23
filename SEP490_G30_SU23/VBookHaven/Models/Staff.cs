using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VBookHaven.Models;

public partial class Staff
{
    [Key]
    public int AccountId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [Column("DOB", TypeName = "date")]
    public DateTime? Dob { get; set; }

    [StringLength(12)]
    public string? IdCard { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Image { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Staff")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("Staff")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
