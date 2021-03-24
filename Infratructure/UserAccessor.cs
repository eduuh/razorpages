using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using UploadandDowloadService.Services;

namespace UploadandDowloadService.Infratructure
{
    public class UserAccessor: IUserAccessor
    {
        private readonly IHttpContextAccessor httpcontextAccessor;

        public UserAccessor(IHttpContextAccessor httpcontextAccessor)
        {
            this.httpcontextAccessor = httpcontextAccessor;
        }

        public string GetCurrentUsername()
        {
            var username = httpcontextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return username;
        }
    }
}