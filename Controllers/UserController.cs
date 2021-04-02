using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.DataAccess;
using Kaizen.Models;
using Kaizen.Models.Enums;
using Kaizen.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UploadandDowloadService.Services;

namespace UploadandDowloadService.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser user;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly AppDbContext _context;

        public UserController(IUser user, IMapper mapper, IServiceProvider provider, AppDbContext context)
        {
            this._provider = provider;
            this._context = context;
            this._mapper = mapper;
            this.user = user;

        }

        [HttpPost("user/login")]
        public async Task<ActionResult<UserSuccessResponse>> Login([FromBody] UserLogin userLogin)
        {
            UserSuccessResponse result = await user.Login(userLogin);

            return Ok(result);
        }


        [HttpPost("user/register")]
        public async Task<ActionResult<UserSuccessResponse>> Register([FromBody] UserRegister userRegister)
        {

            var appuser = new AppUser
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                PhoneNumber = userRegister.PhoneNumber,
                Email = userRegister.Email,
                UserName = userRegister.UserName,
                Region = userRegister.Region,
            };

            if (!await UsersUtilities.RoleExists(userRegister.Role, _provider))
            {
                throw new RestException(HttpStatusCode.NotAcceptable, new { errors = "Please use Roles either Teacher, Parent, Student" });
            }

            appuser.SetRole(userRegister.Role);

            var appuserid = await UsersUtilities.EnsureUser(_provider, userRegister.Password, appuser);

            var result = await UsersUtilities.EnsureRole(_provider, appuserid, userRegister.Role);

            return Ok(appuser);
        }



        [HttpPost("user/register/addtoclass/{classId}")]
        public async Task<ActionResult<UserSuccessResponse>> AddtoClass([FromQuery] string classId)
        {
            var @class = await _context.Classes.FindAsync(classId);

            //check if teacher or student
            if (@class == null)
            {
                return NotFound(new { error = "Class not Found" });
            }
            var @me = await user.GetCurrentLoginDetails();

            if (@me.Type == Role.Parent)
            {
                throw new RestException(HttpStatusCode.NotAcceptable, new { errors = "Parent cannot be added to a Class" });
            }
            if (@me.Type == Role.Teacher || @me.Type == Role.Student)
            {
                @me.Class = @class;
            }

            return Ok(new { success = $"{@me.FirstName} was added to class {@class.Name}" });

        }


        [HttpPost("user/register/addtoclass/{schoolid}")]
        public async Task<ActionResult<UserSuccessResponse>> AddtoSchool([FromQuery] string schoolid)
        {
            var @school = await _context.Schools.FindAsync(schoolid);

            //check if teacher or student
            if (@school == null)
            {
                return NotFound(new { error = "School not Found" });
            }
            var @me = await user.GetCurrentLoginDetails();

            @me.School = @school;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { success = $"{@me.FirstName} was added to class {@school.Name}" });
            }
            catch (Exception)
            {
                throw new Exception("User not added to school");
            }

        }

        [Authorize]
        [HttpGet("user/me")]
        public async Task<ActionResult<AppUser>> CurrentUser()
        {
            var result = await user.GetCurrentLoginDetails();
            return Ok(result);
        }





    }
}