namespace UploadandDowloadService.Models
{
    public class StudentSubjectEnrolled
    {
        public string AppUserId { get; set; }
        public virtual AppUser Appuser { get; set; }

        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        //TODO subject have grades
    }
}