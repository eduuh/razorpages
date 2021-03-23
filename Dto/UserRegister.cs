using System.ComponentModel.DataAnnotations;

namespace UploadandDowloadService.Services
{
    public class UserRegister
    {

       [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}