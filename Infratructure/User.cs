using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Models;
using UploadandDowloadService.Services;

namespace UploadandDowloadService.Infratructure
{
    public class User : IUser
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signinmanager;
        private readonly IJwtToken jwtgenerator;
        private readonly IUserAccessor userAccessor;

        public User(AppDbContext context, UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager, IJwtToken jwtgenerator, IUserAccessor userAccessor)
        {
            this.context = context;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.jwtgenerator = jwtgenerator;
            this.userAccessor = userAccessor;
        }

        public async Task<AppuserDto> GetCurrentLoginDetails()
        {
          var user =  await usermanager.FindByNameAsync(userAccessor.GetCurrentUsername());
          return new AppuserDto{
              LastName = user.LastName,
              FirstName = user.FirstName,
              UserName = user.UserName,
              PhonNumber = user.PhoneNumber,
              Email  = user.Email
          };
        }

        public async Task<UserSuccessResponse> Login(UserLogin userlogin)
        {
            AppUser user = await usermanager.FindByEmailAsync(userlogin.Email);

            if (user == null)
            {
              // TODO create a error handler
              throw new System.Exception("USER NOT FOUND");   
            }

            var result = await signinmanager.CheckPasswordSignInAsync(user, userlogin.Password, false);

            if(result.Succeeded) return new UserSuccessResponse(user.Email, jwtgenerator.createToken(user), user.UserName);

           return new UserSuccessResponse(null, null, null);
            
        }

        public async Task<UserSuccessResponse> Register(UserRegister userregister)
        {
            //check if email is used
            var Exist = await context.Users.AnyAsync(x => x.Email == userregister.Email || x.UserName == userregister.UserName);
            if(Exist){
                //TODO use custom eror handler
                throw new System.Exception("User Email or Username Is taken");
            }

            var user = new AppUser() {
                UserName = userregister.UserName,
                Email = userregister.Email,
                PhoneNumber = userregister.PhoneNumber,
            };
          var result = await usermanager.CreateAsync(user, userregister.Password);

          if (result.Succeeded)
          {
              return new UserSuccessResponse(user.Email, jwtgenerator.createToken(user), user.UserName);
          }

          return new UserSuccessResponse(null, null, null);
        }
    }
}