using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Models;
using uploaddownloadfiles.Areas.Identity.Data;

namespace uploaddownloadfiles.Background
{
    public class SeedDataHostedService : IHostedService
    {

        private readonly UserManager<AppUser> usermanager;
        private readonly IServiceProvider serviceProvider;
        private readonly AppDbContext context;


        public SeedDataHostedService(IServiceProvider serviceProvider, AppDbContext context)
        {

            this.usermanager = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            this.serviceProvider = serviceProvider;
            this.context = context;

        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DummyDataSeedData.InitializeUsers(serviceProvider);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
