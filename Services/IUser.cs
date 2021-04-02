using Kaizen.Models;
using System.Threading.Tasks;
using UploadandDowloadService.Controllers;

namespace  UploadandDowloadService.Services
{
    public interface IUser
    {
         Task<UserSuccessResponse> Login(UserLogin userlogin);
         Task<UserSuccessResponse> Register(UserRegister userregister);
         Task<AppUser> GetCurrentLoginDetails();
    }
}