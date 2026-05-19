using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class Author : Entity
{
    [Display(Name = "Автор")]
    public string? Pseudonym { get; set; }

    public DateOnly birthdate { get; set; }
}
