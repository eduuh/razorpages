using System.Threading.Tasks;
using UploadandDowloadService.Controllers;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Services
{
    public interface IUser
    {
         Task<UserSuccessResponse> Login(UserLogin userlogin);
         Task<UserSuccessResponse> Register(UserRegister userregister);
        Task<AppUser> GetCurrentLoginDetails();
    }
}