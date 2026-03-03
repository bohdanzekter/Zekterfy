using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Genre : Entity
{
    public string? Name { get; set; }

    public string? Info { get; set; }
}
