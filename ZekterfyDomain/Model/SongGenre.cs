using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class SongGenre
{
    public int? SongId { get; set; }

    public int? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual Song? Song { get; set; }
}
