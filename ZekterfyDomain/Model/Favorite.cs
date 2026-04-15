using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class Favorite : Entity
{
    [Display(Name = "Додано")]
    public DateTime? Added { get; set; }

    [Display(Name = "ID Користувача")]
    public string? UserId { get; set; }

    [Display(Name = "ID Пісні")]
    public int? SongId { get; set; }

    [Display(Name = "Назва пісні")]
    public virtual Song? Song { get; set; }
}
