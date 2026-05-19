using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class History: Entity
{
    public string? UserId { get; set; }

    public int? SongId { get; set; }

    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    public virtual Song? Song { get; set; }
}
