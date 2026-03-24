using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Report : Entity
{
    public string? UserId { get; set; }

    public int? SongId { get; set; }

    public string? Reason { get; set; }

    public bool? Status { get; set; }

    public virtual Song? Song { get; set; }

    public virtual User? User { get; set; }
}
