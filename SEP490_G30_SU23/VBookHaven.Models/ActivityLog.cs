using System;
using System.Collections.Generic;

namespace VBookHaven.Models;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int? ActorId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Action { get; set; }

    public string? ActivitySummary { get; set; }

    public string? ItemsId { get; set; }

    public string? TableName { get; set; }

    public string? ActivityDetails { get; set; }

    public virtual Staff? Actor { get; set; }
}
