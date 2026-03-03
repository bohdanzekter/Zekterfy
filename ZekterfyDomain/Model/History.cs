using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class History
{
    public int UserId { get; set; }

    public int? SongId { get; set; }

    public DateOnly? PlayedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
