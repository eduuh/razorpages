using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UploadandDowloadService.enums;

namespace UploadandDowloadService.Models
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
        public Subject Subject { get; set; }
        public TrainingSubject TraingSubject { get; set; }
        public ContentType ContentType { get; set; }
    }
}