using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UploadandDowloadService.Models;
using UploadandDowloadService.Services;

namespace UploadandDowloadService.Filters
{
    public class UserNameRequirementHandler : AuthorizationHandler<UserNameRequirements>
    {
        private readonly IUserAccessor _useraccessor;
        private readonly UserManager<AppUser> _userManager;

        public UserNameRequirementHandler(IUserAccessor useraccessor, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._useraccessor = useraccessor;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserNameRequirements requirement)
        {
            requirement.Username = _useraccessor.GetCurrentUsername();
            var user = await _userManager.FindByNameAsync(requirement.Username);
            string[] roles = (await _userManager.GetRolesAsync(user)).ToArray();
            if (roles.Contains("Admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

        }
    }
}