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
        public string image { get; set; }
        public string ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }
        public virtual ICollection<AppUser> Stakeholders { get; set; } = new List<AppUser>();
        public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}