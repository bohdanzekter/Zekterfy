using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Song : Entity
{
    public int? Lenght { get; set; }

    public int? NumOfStreams { get; set; }

    public string? Name { get; set; }

    public int? AlbumId { get; set; }

    //here should be genreId, have to 
    public string? GenreName { get; set; }

    public virtual Album? Album { get; set; }
}
