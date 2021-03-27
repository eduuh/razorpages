using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Filters;
using UploadandDowloadService.Models;
using UploadandDowloadService.Services;
using uploaddownloadfiles.Models;

namespace UploadandDowloadService.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser user;

        public UserController(IUser user)
        {
            this.user = user;

        }

        [HttpPost("user/login")]
        public async Task<ActionResult<UserSuccessResponse>> Login([FromBody]UserLogin userLogin)
        {
            UserSuccessResponse result = await user.Login(userLogin);

                 return Ok(result);
        }


        [HttpPost("user/register")]
        public async Task<ActionResult<UserSuccessResponse>> Register([FromBody]UserRegister userRegister)
        {
            var result = await user.Register(userRegister);
           return Ok(result);
        }


        [HttpGet("user/me")]
        public async Task<ActionResult<AppUser>> CurrentUser()
        {
            var result = await user.GetCurrentLoginDetails();
            return Ok(result);
        }





    }
}