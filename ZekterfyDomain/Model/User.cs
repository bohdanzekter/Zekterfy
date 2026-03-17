using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ZekterfyDomain.Model;

public partial class User : IdentityUser
{
    public int Year { get; set; }

    [Display(Name = "Посилання на аватар")]
    public string? AvatarUrl { get; set; }

    [Display(Name = "Історія")]
    public virtual History? History { get; set; }
}
