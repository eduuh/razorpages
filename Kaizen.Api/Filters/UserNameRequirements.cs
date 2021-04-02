using Microsoft.AspNetCore.Authorization;

namespace UploadandDowloadService.Filters
{
    public class UserNameRequirements : IAuthorizationRequirement
    {
        public string Username { get; set; }
        public UserNameRequirements(string username)
        {
            this.Username = username;

        }
    }
}