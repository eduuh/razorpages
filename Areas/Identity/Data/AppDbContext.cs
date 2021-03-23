using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Areas.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Content> Contents { get; set; }
    }
}
