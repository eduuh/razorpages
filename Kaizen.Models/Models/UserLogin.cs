using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models
{
    public class UserLogin
    {

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Required]
        [EmailAddress]
        public string Email { get; set;} 
    }
}