using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UploadandDowloadService.Areas.Identity;

public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly IConfiguration config;
  
    public AppContextFactory() { }
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectstring = "Server=tcp:kaizenserver.database.windows.net,1433;Initial Catalog=kaizen;Persist Security Info=False;User ID=kaizenadmin;Password=pK}w/5cfF<RwvRjWGSB5RJ=^^;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        optionsBuilder.UseSqlServer(connectstring);
        return new AppDbContext(optionsBuilder.Options);
    }
}