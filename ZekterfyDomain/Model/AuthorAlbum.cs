using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class AuthorAlbum
{
    public int AuthorId { get; set; }

    public int AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;
}
