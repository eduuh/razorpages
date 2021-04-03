using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Kaizen.Models;

namespace Kaizen.Utilities
{
    public static class UsersUtilities
    {
        public static async Task<string> EnsureUser(IServiceProvider provider, string testUserPw, AppUser appuser)
        {
            var usermananger = provider.GetService<UserManager<AppUser>>();

            var user = await usermananger.FindByNameAsync(appuser.UserName);
            var useremail = await usermananger.FindByEmailAsync(appuser.Email);

            if (user != null)
            {
                throw new RestException(HttpStatusCode.Conflict, new { errors = "Username is already Registered , Please use anotherone" });
            }

            if (useremail != null)
            {
                throw new RestException(HttpStatusCode.Conflict, new { errors = "Email is already Registered, Please use anotherOne" });
            }

            if (user == null && useremail == null)
            {
                user = appuser;
                await usermananger.CreateAsync(appuser, testUserPw);
            }
            return user.Id;
        }


        public static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult ir = null;

            var usermananger = serviceProvider.GetService<UserManager<AppUser>>();
            var user = await usermananger.FindByIdAsync(uid);

            if (user == null) throw new Exception("The User was not registered successfully");

            ir = await usermananger.AddToRoleAsync(user, role);
            return ir;
        }

        public static async Task<bool> RoleExists(string role, IServiceProvider provider)
        {
            var rolemanager = provider.GetService<RoleManager<IdentityRole>>();
            return await rolemanager.RoleExistsAsync(role);
        }
    }
}