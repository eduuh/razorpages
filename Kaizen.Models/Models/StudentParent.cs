namespace Kaizen.Models
{
    public class StudentParent
    {
        public string StudentId { get; set; }
        public virtual AppUser Student { get; set; }
        public string ParentId { get; set; }
        public virtual AppUser Parent { get; set; }
    }
}