using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace UploadandDowloadService.Models
{
    public class AppUser : IdentityUser
    {
    public string FirstName { get; set; }
    public string LastName { get; set; }
     public bool isStudent { get; set; }
     public bool isTeacher { get; set; }
     public bool isParent {get; set;}
     public ICollection<School> School { get; set; }
    }
}