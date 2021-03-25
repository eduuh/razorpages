using System.Collections.Generic;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Models
{
    public class TrainingSubject
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Role ContentTarget { get; set; }
        public ICollection<Content>  Contents { get; set; }
    }
}