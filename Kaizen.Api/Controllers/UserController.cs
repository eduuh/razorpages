using System;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UploadandDowloadService.Controllers
{
    [Route("user/")]
    public class UserController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper _mapper;

        public UserController(IUnitofWork unitofWork, IMapper mapper)
        {
            this._mapper = mapper;
            this.unitofWork = unitofWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = unitofWork.AppUser.GetAll() });
        }

        [HttpPost]
        public IActionResult LockUnLock([FromBody] string id)
        {
            var objectfromdb = unitofWork.AppUser.GetFirstOrDefault(u => u.Id == id);

            if (objectfromdb == null)
            {
                return Json(new { success = false, message = "Error while Locking and Unlocking" });
            }

            if (objectfromdb.LockoutEnd != null && objectfromdb.LockoutEnd > DateTime.Now)
            {
                objectfromdb.LockoutEnd = DateTime.Now;
            }
            else
            {

                objectfromdb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            unitofWork.Save();
            return Json(new { success = true, message = "Operation Succesfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserSuccessResponse>> Login([FromBody] UserLogin userLogin)
        {
            UserSuccessResponse result = await unitofWork.AppUser.Login(userLogin);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserSuccessResponse>> Register([FromBody] UserRegister userRegister)
        {
            var data = await unitofWork.AppUser.Register(userRegister);
            unitofWork.Save();
            return Json(new { data = data });
        }



        // [HttpPost("user/register/addtoclass/{classId}")]
        // public async Task<ActionResult<UserSuccessResponse>> AddtoClass([FromQuery] string classId)
        // {
        //     var @class = await _context.Classes.FindAsync(classId);

        //     //check if teacher or student
        //     if (@class == null)
        //     {
        //         return NotFound(new { error = "Class not Found" });
        //     }
        //     var @me = await user.GetCurrentLoginDetails();

        //     if (@me.Type == Role.Parent)
        //     {
        //         throw new RestException(HttpStatusCode.NotAcceptable, new { errors = "Parent cannot be added to a Class" });
        //     }
        //     if (@me.Type == Role.Teacher || @me.Type == Role.Student)
        //     {
        //         @me.Class = @class;
        //     }

        //     return Ok(new { success = $"{@me.FirstName} was added to class {@class.Name}" });
        // }


        // [HttpPost("user/register/addtoclass/{schoolid}")]
        // public async Task<ActionResult<UserSuccessResponse>> AddtoSchool([FromQuery] string schoolid)
        // {
        //     var @school = await _context.Schools.FindAsync(schoolid);

        //     //check if teacher or student
        //     if (@school == null)
        //     {
        //         return NotFound(new { error = "School not Found" });
        //     }
        //     var @me = await user.GetCurrentLoginDetails();

        //     @me.School = @school;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //         return Ok(new { success = $"{@me.FirstName} was added to class {@school.Name}" });
        //     }
        //     catch (Exception)
        //     {
        //         throw new Exception("User not added to school");
        //     }

        // }

        // [Authorize]
        // [HttpGet("user/me")]
        // public async Task<ActionResult<AppUserDto>> CurrentUser()
        // {
        //     var data = await unitofWork.AppUser.GetCurrentLoginDetails();
        //     var mapped = _mapper.Map<AppUser, AppuserDto>(data);
        //     return Json(new { data = await unitofWork.AppUser.GetCurrentLoginDetails() });
        // }





    }
}