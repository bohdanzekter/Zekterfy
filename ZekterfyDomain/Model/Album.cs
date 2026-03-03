using System;
using System.Collections.Generic;

namespace ZekterfyDomain.Model;

public partial class Album : Entity
{
    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
