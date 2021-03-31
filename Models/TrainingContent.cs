using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadandDowloadService.Models
{
    public class TrainingSubject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Description { get; set; }
        public Role ContentTarget { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}