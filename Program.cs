using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Areas.Identity.Data;
using UploadandDowloadService.Models;
using uploaddownloadfiles.Areas.Identity.Data;

namespace UploadandDowloadService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                try
                {
                    var context = service.GetRequiredService<AppDbContext>();
                    var usermanager = service.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();

                    SeedData.Initialize(service, "Pa$$w0rd54").Wait();
                    DummyDataSeedData.InitializeUsers(service).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
