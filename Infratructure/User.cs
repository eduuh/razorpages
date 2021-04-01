using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Controllers;
using UploadandDowloadService.Data;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Models;
using UploadandDowloadService.Services;
using uploaddownloadfiles.Models;

namespace UploadandDowloadService.Infratructure
{
    public class User : IUser
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signinmanager;
        private readonly IJwtToken jwtgenerator;
        private readonly IUserAccessor userAccessor;
        private readonly RoleManager<IdentityRole> roleManager;

        public User(AppDbContext context, UserManager<AppUser> usermanager,
        SignInManager<AppUser> signinmanager, IJwtToken jwtgenerator, IUserAccessor userAccessor,
        RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.jwtgenerator = jwtgenerator;
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
        }

        public async Task<AppUser> GetCurrentLoginDetails()
        {
            var user = await usermanager.FindByNameAsync(userAccessor.GetCurrentUsername());
            return user;  //TODO configurer nmap
        }

        public async Task<UserSuccessResponse> Login(UserLogin userlogin)
        {
            AppUser user = await usermanager.FindByEmailAsync(userlogin.Email);

            if (user == null)
            {
                // TODO create a error handler
                throw new RestException(System.Net.HttpStatusCode.Unauthorized, new { message = "Not Authorized" });
            }

            var result = await signinmanager.CheckPasswordSignInAsync(user, userlogin.Password, false);

            if (result.Succeeded) return new UserSuccessResponse(user.Email, jwtgenerator.createToken(user), user.UserName);

            throw new RestException(System.Net.HttpStatusCode.Unauthorized, new { message = "Please Use the correct Password" });

        }

        public async Task<UserSuccessResponse> Register(UserRegister userregister)
        {
            //check if email is used
            var Exist = await context.Users.AnyAsync(x => x.Email == userregister.Email || x.UserName == userregister.UserName);
            if (Exist)
            {
                //TODO use custom eror handler
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "User Email or Username Is taken");
            }

            var user = new AppUser()
            {
                UserName = userregister.UserName,
                Email = userregister.Email,
                PhoneNumber = userregister.PhoneNumber,
            };

            var result = await usermanager.CreateAsync(user, userregister.Password);
            var ensurerole = await EnsureRole(user, userregister.Role);
            if (!ensurerole.Succeeded)
            {
                await usermanager.DeleteAsync(user);
                throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { message = "User registration not successfull Please Try again" });
            }

            if (result.Succeeded)
            {
                return new UserSuccessResponse(user.Email, jwtgenerator.createToken(user), user.UserName);
            }

            return new UserSuccessResponse(null, null, null);
        }


        private async Task<IdentityResult> EnsureRole(AppUser user, string role)
        {
            if (roleManager == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, "Rolemanager Error");
            }
            if (!await roleManager.RoleExistsAsync(role))
            {

                await roleManager.CreateAsync(new IdentityRole(role));
                //Todo Precreate roles and activate this line =>  throw new RestException(System.Net.HttpStatusCode.NotFound,"Role does not Exist, either Student, Teacher, Admin");
            }

            if (user == null) throw new RestException(System.Net.HttpStatusCode.NotFound, new { message = "The User is Not succeffuly registerd" });
            var ir = await usermanager.AddToRoleAsync(user, role.ToString());

            return ir;
        }
    }
}