using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? FullName { get; set; }

    public DateTime? Dob { get; set; }

    public string? IdCard { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Image { get; set; }

    public bool? IsMale { get; set; }

    public string? AccountId { get; set; }
    public virtual ApplicationUser? Account { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

}
