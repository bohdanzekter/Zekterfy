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

    public string? Name { get; set; }

    public int? AlbumId { get; set; }

    //here should be genreId, have to 
    public string? GenreName { get; set; }

    public virtual Album? Album { get; set; }
}
