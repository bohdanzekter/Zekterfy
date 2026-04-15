using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class Song : Entity
{

    [Required(ErrorMessage = "Довжина пісні обов'язкова")]
    [Display(Name = "Довжина пісні")]
    public int? Lenght { get; set; }

    [Display(Name = "Кількість прослуховувать")]
    public int? NumOfStreams { get; set; }

    [Required(ErrorMessage = "Назва пісні обов'язкова")]
    [Display(Name = "Назва пісні")]
    public string? Name { get; set; }

    [Display(Name = "id альбому")]
    public int? AlbumId { get; set; }

    //[Required(ErrorMessage = "Жанр пісні обов'язковий")]
    [Display(Name = "Жанр пісні")]
    public int? GenreId { get; set; }

    [Display(Name = "Альбом")]
    public virtual Album? Album { get; set; }

    [Display(Name = "Жанр")]
    public virtual Genre? Genre { get; set; }
}
