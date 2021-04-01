using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadandDowloadService.Models
{
    public class Contact
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string Pobox { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}