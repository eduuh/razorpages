using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Models
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Motto { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ICollection<AppUser> Stakeholders { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}