using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Follower : Entity
{
    public string? UserId { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual User? User { get; set; }
}
