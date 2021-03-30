using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Areas.Identity.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider provider, string testuserpw)
        {
            using (var context = new AppDbContext(provider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {

                var adminID = await EnsureUser(provider, testuserpw,
                 new AppUser
                 {
                     UserName = "admin@contoso.com",
                     FirstName = "Admin",
                     LastName = "Kaizen",
                 });
                await EnsureRole(provider, adminID, Role.Admin);

                var managerId = await EnsureUser(provider, testuserpw, new AppUser
                {
                    UserName = "management@contoso.com",
                    FirstName = "Management",
                    LastName = "Kaizen",
                });
                await EnsureRole(provider, managerId, Role.Manager);
            }
        }

        public static async Task<string> EnsureUser(IServiceProvider provider, string testUserPw, AppUser appuser)
        {
            var usermananger = provider.GetService<UserManager<AppUser>>();

            var user = await usermananger.FindByNameAsync(appuser.UserName);
            if (user == null)
            {
                user = appuser;
                await usermananger.CreateAsync(appuser, testUserPw);
            }
            if (user == null)
            {
                throw new Exception("The password is probably not strong enough");
            }
            return user.Id;
        }


        public static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, Role role)
        {
            IdentityResult ir = null;
            var rolemanager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (rolemanager == null)
            {
                throw new Exception("rolemanger error");
            }
            if (!await rolemanager.RoleExistsAsync(role.ToString()))
            {
                ir = await rolemanager.CreateAsync(new IdentityRole(role.ToString()));
            }

            var usermananger = serviceProvider.GetService<UserManager<AppUser>>();
            var user = await usermananger.FindByIdAsync(uid);

            if (user == null) throw new Exception("The testUser password is not string enough");
            ir = await usermananger.AddToRoleAsync(user, role.ToString());
            return ir;
        }
    }
}