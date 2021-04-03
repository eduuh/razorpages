using System;
using System.Linq;
using System.Security.Claims;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;

namespace Kaizen.DataAccess.Data.Repository
{
    public class UserAccessor : IUserAccessor
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