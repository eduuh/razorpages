using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Models;

namespace uploaddownloadfiles.Background
{
    public class SeedDataHostedService : IHostedService
    {

        private readonly UserManager<AppUser> usermanager;
        private readonly AppDbContext context;


        public SeedDataHostedService(UserManager<AppUser> usermanager, AppDbContext context)
        {

            this.usermanager = usermanager;
            this.context = context;

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
