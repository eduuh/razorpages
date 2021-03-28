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
        public static async Task Initialize(IServiceProvider provider , string testuserpw){
            using (var context = new AppDbContext(provider.GetRequiredService<DbContextOptions<AppDbContext>>())){
               var adminID = await EnsureUser(provider, testuserpw, "admin@kaizen.com");
               await EnsureRole(provider, adminID, Role.Admin);

               var managerId = await EnsureUser(provider, testuserpw, "manager@contoso.com");
               await EnsureRole(provider, managerId, Role.Manager);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider provider , string testUserPw, string username){
            var usermananger = provider.GetService<UserManager<AppUser>>();

            var user = await usermananger.FindByNameAsync(username);
            if(user==null){
                user = new AppUser {
                    UserName = username,
                    EmailConfirmed = true,
                };
                await usermananger.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
              throw new Exception("The password is probably not strong enough");   
            }

            return user.Id;
        }


        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, Role role){
            IdentityResult ir =  null;
            var rolemanager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if(rolemanager == null){
                throw new Exception("rolemanger error");
            }
            if(!await rolemanager.RoleExistsAsync(role.ToString())){
                ir = await rolemanager.CreateAsync(new IdentityRole(role.ToString()));
            }

            var usermananger = serviceProvider.GetService<UserManager<AppUser>>();
            var user = await usermananger.FindByNameAsync(uid);

            if(user == null) throw new Exception("The testUser password is not string enough");
            ir = await usermananger.AddToRoleAsync(user, role.ToString());
            return ir;
        }
    }
}