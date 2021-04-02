using Kaizen.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: HostingStartup(typeof(UploadandDowloadService.Data.IdentityHostingStartup))]
namespace UploadandDowloadService.Data
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



                //var builder = services.AddIdentityCore<AppUser>();
                //var identitybuilder = new IdentityBuilder(builder.UserType, builder.Services);
                //identitybuilder.AddRoles<IdentityRole>();
                //identitybuilder.AddEntityFrameworkStores<AppDbContext>();
                //identitybuilder.AddSignInManager<SignInManager<AppUser>>();

            });
        }
    }
}