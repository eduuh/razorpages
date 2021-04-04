namespace Kaizen.Models.Dto
{
    public class CultureReportDto
    {
        public string Id { get; set; }
        public string Incident { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public string Town { get; set; }
        public string OccurrenceTimes { get; set; }
        public string GenderAffected { get; set; }
    }
}