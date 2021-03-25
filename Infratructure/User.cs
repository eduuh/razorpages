using System;
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

          var role = userregister.Role;
          var result = await usermanager.CreateAsync(user, userregister.Password);
          var ensurerole = await EnsureRole(user, (Role)Enum.Parse(typeof(Role),role));

          if (result.Succeeded)
          {
              return new UserSuccessResponse(user.Email, jwtgenerator.createToken(user), user.UserName);
          }

          return new UserSuccessResponse(null, null, null);
        }


        private async Task<IdentityResult> EnsureRole(AppUser user, Role role){
            if(roleManager== null){
                throw new System.Exception("Rolemanager Eror");
            }
            if(!await roleManager.RoleExistsAsync(role.ToString())){
                throw new System.Exception("Role does not Exist");
            }
          
          if(user == null) throw new System.Exception("The User is Not succeffuly registerd");
          var ir = await usermanager.AddToRoleAsync(user, role.ToString());

          return ir;
    }
    }
}