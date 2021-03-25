using UploadandDowloadService.enums;

namespace UploadandDowloadService.Models
{
    public class Content
    {
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