using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Models;

namespace uploaddownloadfiles.Areas.Identity.Data
{
    public static class DummyDataSeedData
    {
        public static async Task InitializeUsers(UserManager<AppUser> usermanager, AppDbContext context)
        {
               
        }
    }
}
