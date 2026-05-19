using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model;

public partial class Album : Entity
{
    [Display(Name = "Назва альбому")]
    public string Name { get; set; } = null!;

    [Display(Name = "ID автора")]
    public int AuthorId { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
