using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UploadandDowloadService.Infratructure;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Areas.Identity.Data
{
    public static class SeedData
    {

        public static async Task CreateDefaultRoles(IServiceProvider provider)
        {
            var rolemanager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await rolemanager.RoleExistsAsync(Role.Admin.ToString()))
            {
                await rolemanager.CreateAsync(new IdentityRole() { Name = Role.Admin.ToString() });
                await rolemanager.CreateAsync(new IdentityRole() { Name = Role.Student.ToString() });
                await rolemanager.CreateAsync(new IdentityRole() { Name = Role.Teacher.ToString() });
                await rolemanager.CreateAsync(new IdentityRole() { Name = Role.Parent.ToString() });
            }
        }
        public static async Task Initialize(IServiceProvider provider, string testuserpw)
        {
            using (var context = new AppDbContext(provider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {

                var adminID = await UsersUtilities.EnsureUser(provider, testuserpw,
                 new AppUser
                 {
                     Email = "admin@contoso.com",
                     FirstName = "Admin",
                     LastName = "Kaizen",
                     UserName = "admin",
                     isAdmin = true
                 });
                await UsersUtilities.EnsureRole(provider, adminID, Role.Admin.ToString());

                var managerId = await UsersUtilities.EnsureUser(provider, testuserpw, new AppUser
                {
                    Email = "management@contoso.com",
                    FirstName = "Management",
                    LastName = "Kaizen",
                    UserName = "kaizen",
                    isAdmin = true
                });
                await UsersUtilities.EnsureRole(provider, managerId, Role.Manager.ToString());
            }
        }

    }
}