using UploadandDowloadService.Models;

namespace UploadandDowloadService.Services
{
    public interface IJwtToken
    {
        string createToken(AppUser user);
    }
}