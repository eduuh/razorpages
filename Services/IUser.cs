using System.Threading.Tasks;
using UploadandDowloadService.Dto;

namespace UploadandDowloadService.Services
{
    public interface IUser
    {
         Task<UserSuccessResponse> Login(UserLogin userlogin);
         Task<UserSuccessResponse> Register(UserRegister userregister);
        Task<AppuserDto> GetCurrentLoginDetails();
    }
}