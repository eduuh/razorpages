using System.ComponentModel.DataAnnotations;

namespace UploadandDowloadService.Dto
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