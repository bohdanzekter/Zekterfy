using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Favorite
{
    public DateTime? Added { get; set; }

    public string? UserId { get; set; }

    public int? SongId { get; set; }

    public virtual User? User { get; set; }
}
