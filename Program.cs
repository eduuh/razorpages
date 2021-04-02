using System;
using Kaizen.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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
                    // var usermanager = service.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();
                    //    SeedData.CreateDefaultRoles(service).Wait();
                    //  SeedData.Initialize(service, "Pa$$w0rd54").Wait();
                    // DummyDataSeedData.InitializeUsers(service).Wait();
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
