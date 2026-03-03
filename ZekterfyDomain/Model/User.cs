using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class User : Entity
{
    public string? Password { get; set; }

    public int? FolowersCount { get; set; }

    public int? FolowsCount { get; set; }

    public int? PreferedGenreId { get; set; }

    public bool? Listening { get; set; }

    public bool? IsAdmin { get; set; }

    public string? AvatarUrl { get; set; }

    public virtual History? History { get; set; }
}
