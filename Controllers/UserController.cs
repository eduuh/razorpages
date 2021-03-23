using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Filters;
using UploadandDowloadService.Services;

namespace UploadandDowloadService.Controllers
{

    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser user;

        public UserController(IUser user)
        {
            this.user = user;

        }

        [HttpPost("user/login")]
        public async Task<CustomActionResult<UserSuccessResponse>> Login(UserLogin userLogin)
        {
            UserSuccessResponse result = await user.Login(userLogin);

            if (result.Token != null) {
                 return new CustomActionResult<UserSuccessResponse>(result, HttpStatusCode.OK);
            }
            else
            {
                return new CustomActionResult<UserSuccessResponse>(result, HttpStatusCode.BadRequest);
            };
        }


        [HttpPost("user/register")]
        public async Task<CustomActionResult<UserSuccessResponse>> Register(UserRegister userRegister)
        {
            var result = await user.Register(userRegister);

            if (result.Token != null) {
               return new CustomActionResult<UserSuccessResponse>(result, HttpStatusCode.OK);
             }
            else
            {
                return new CustomActionResult<UserSuccessResponse>(result, HttpStatusCode.BadRequest);
            };
        }





    }
}