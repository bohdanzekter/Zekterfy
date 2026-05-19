using System.ComponentModel.DataAnnotations;

namespace ZekterfyDomain.Model
{
    public abstract class Entity
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
    }
}
