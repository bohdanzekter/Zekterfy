using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class Genre : Entity
{
    [Required(ErrorMessage = "Назва обов'язкова")]
    [Display(Name = "Назва жанру")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Інформація обов'язкова")]
    [Display(Name = "Інформація про жанр")]
    public string? Info { get; set; }
}
