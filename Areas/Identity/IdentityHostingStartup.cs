using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UploadandDowloadService.Models;


[assembly: HostingStartup(typeof(UploadandDowloadService.Areas.Identity.IdentityHostingStartup))]
namespace UploadandDowloadService.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AzureSqlServiceConnectionString"),
                        o => o.EnableRetryOnFailure());

                    options.UseLazyLoadingProxies();
                }

                        );



                var builder = services.AddIdentityCore<AppUser>();
                var identitybuilder = new IdentityBuilder(builder.UserType, builder.Services);
                identitybuilder.AddRoles<IdentityRole>();
                identitybuilder.AddEntityFrameworkStores<AppDbContext>();
                identitybuilder.AddSignInManager<SignInManager<AppUser>>();

            });
        }
    }
}