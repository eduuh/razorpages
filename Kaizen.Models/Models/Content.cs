using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Models
{
    public class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string type { get; set; }
        public string BlobUrl { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual TrainingSubject TraingSubject { get; set; }
        public string ContentType { get; set; }
    }
}