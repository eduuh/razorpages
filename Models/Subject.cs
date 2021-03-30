using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadandDowloadService.Models
{
    public class Subject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Class Class { get; set; }
        public AppUser Teacher { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<AppUser> Students { get; set; }
        public ICollection<StudentSubjectEnrolled> StudentEnrolled { get; set; }

    }
}