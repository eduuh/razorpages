using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Models
{
    public class Subject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual Class Class { get; set; }
        public virtual AppUser Teacher { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<AppUser> Students { get; set; }
        public virtual ICollection<StudentSubjectEnrolled> StudentEnrolled { get; set; }

    }
}