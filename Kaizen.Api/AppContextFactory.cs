using Kaizen.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly IConfiguration config;

    public AppContextFactory() { }
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectstring = "Server=tcp:afrilearn-server.database.windows.net,1433;Initial Catalog=kaizen;Persist Security Info=False;User ID=Humphryshikunzi;Password=2288Shiks.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        optionsBuilder.UseSqlServer(connectstring);
        return new AppDbContext(optionsBuilder.Options);
    }
}
