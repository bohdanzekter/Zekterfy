using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class SongAuthor
{
    public int? SongId { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Song? Song { get; set; }
}
