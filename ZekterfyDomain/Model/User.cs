using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class User : Entity
{
    [Required(ErrorMessage = "Пароль обов'язковий")]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [Display(Name = "Кількість підписок")]
    public int? FolowersCount { get; set; }

    [Display(Name = "Кількість підписників")]
    public int? FolowsCount { get; set; }

    [Display(Name = "Приорітетний жанр")]
    public int? PreferedGenreId { get; set; }

    [Display(Name = "Прослуховує")]
    public bool? Listening { get; set; }

    [Display(Name = "Адмін")]
    public bool? IsAdmin { get; set; }

    [Display(Name = "Посилання на аватар")]
    public string? AvatarUrl { get; set; }

    [Display(Name = "Історія")]
    public virtual History? History { get; set; }
}
