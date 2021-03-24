namespace UploadandDowloadService.Models
{
    public class StudentSubjectEnrolled
    {
        public string AppUserId { get; set; }
        public AppUser Appuser { get; set; }

        public string SubjectId { get; set; }
        public Subject Subject { get; set; }

        //TODO subject have grades
    }
}