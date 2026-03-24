using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Queue : Entity
{
    public string? UserId { get; set; }

    public int? SongId { get; set; }

    public int? Position { get; set; }

    public virtual User? User { get; set; }
}
