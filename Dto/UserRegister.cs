using System.ComponentModel.DataAnnotations;

namespace UploadandDowloadService.Services
{
    public class UserRegister
    {

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Username should be Less than 20")]
        public string UserName { get; set; }

        public string Role { get; set; }
        public string Region { get; set; }
    }
}