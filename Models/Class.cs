using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class Class
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<AppUser> students { get; set; }

    }
}