using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class Class
    {
        public string Id { get; set; }
        public AppUser ClassTeacher { get; set; }
        public AppUser ClassPrefect { get; set; }
        public ICollection<AppUser> students { get; set; }
    }
}