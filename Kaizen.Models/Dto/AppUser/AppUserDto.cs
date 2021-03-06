namespace UploadandDowloadService.Dto.AppUser
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string LockoutEnd { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

    }

}