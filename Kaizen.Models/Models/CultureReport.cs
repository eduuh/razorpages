using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Models.Models
{
    public class CultureReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Incident { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public string Town { get; set; }
        public string OccurrenceTimes { get; set; }
        public string GenderAffected { get; set; }
    }
}