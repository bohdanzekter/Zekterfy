using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Author : Entity
{
    public string? Pseudonym { get; set; }
}
