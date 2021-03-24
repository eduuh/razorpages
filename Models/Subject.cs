using System.Collections.Generic;

namespace UploadandDowloadService.Models
{
    public class Subject
    {
        public string Id { get; set; }
        public AppUser Teacher { get; set; }
        public string TotalMarks {get; set;}
        public ICollection<Content> Contents {get; set;}
        public ICollection<AppUser> Students {get; set;}
        public ICollection<StudentSubjectEnrolled> StudentEnrolled {get; set;}

    }
}