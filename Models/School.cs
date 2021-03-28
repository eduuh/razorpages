using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class School
    {
    
      public string Id { get; set; }
      public string Name { get; set; }
      public string Motto { get; set; }

      public  Contact Contact { get; set; }
       public ICollection<AppUser> Stakeholders {get; set;}
     
    
    }
}