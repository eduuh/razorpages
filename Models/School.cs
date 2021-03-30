using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadandDowloadService.Models
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }

        public Contact Contact { get; set; }
        public ICollection<AppUser> Stakeholders { get; set; }
        public ICollection<Class> Classes { get; set; }


    }
}