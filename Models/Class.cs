using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class Class
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<AppUser> students { get; set; }
    }
}