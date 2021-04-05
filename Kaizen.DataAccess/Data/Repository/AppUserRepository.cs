using System.Threading.Tasks;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Kaizen.Utilities.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.DataAccess.Data.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signinmanager;
        private readonly IUserAccessor userAccessor;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtToken _jwtgenerator;

        public AppUserRepository(
            AppDbContext context,
            UserManager<AppUser> usermanager,
            SignInManager<AppUser> signinmanager,
            IJwtToken jwtgenerator,
            IUserAccessor userAccessor,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            this._jwtgenerator = jwtgenerator;
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
            this.context = context;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        public async Task<AppUser> GetCurrentLoginDetails()
        {
            var user = await usermanager.FindByNameAsync(userAccessor.GetCurrentUsername());
            return user;  //TODO configurer nmap
        }

        public async Task<UserSuccessResponse> Login(UserLogin userlogin)
        {
            if (userlogin.Email == null && userlogin.Password == null)
            {
                throw new RestException(System.Net.HttpStatusCode.Unauthorized, new { message = "Please Input an Email and Pasword" });
            }
            AppUser user = await usermanager.FindByEmailAsync(userlogin.Email);

            if (user == null)
            {
                // TODO create a error handler
                throw new RestException(System.Net.HttpStatusCode.Unauthorized, new { message = "Not Authorized" });
            }

            var result = await signinmanager.CheckPasswordSignInAsync(user, userlogin.Password, false);

            if (result.Succeeded) return new UserSuccessResponse(user.Email, _jwtgenerator.createToken(user), user.UserName);

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
            if (ensurerole.Succeeded == false)
            {
                await usermanager.DeleteAsync(user);
                throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { message = "User registration not successfull Please Try again" });
            }

            if (result.Succeeded)
            {
                return new UserSuccessResponse(user.Email, _jwtgenerator.createToken(user), user.UserName);
            }
            throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { message = "User registration not successfull Please Try again" });
        }

        public void Update(AppUser user)
        {
            var objectoupdate = Get(user.Id);
            if (objectoupdate.SchoolId == null && user.SchoolId != null)
            {
                objectoupdate.SchoolId = user.SchoolId;
            }
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