namespace UploadandDowloadService.Models
{
    public class StudentParent
    {
        public  string StudentId { get; set; }
        public AppUser Student { get; set; }
        public string ParentId { get; set; }
        public AppUser Parent {get; set;}
    }
}