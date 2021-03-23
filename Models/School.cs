using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class School
    {
    
      public string Id { get; set; }
      public string Name { get; set; }
      public int Age { get; set;}
      public ICollection<AppUser> Parents { get; set;}
      public ICollection<AppUser> Students { get; set; }
      public ICollection<AppUser> Teachers {get; set;}
    }
}