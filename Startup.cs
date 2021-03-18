using System.Linq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uploaddownloadfiles.Interface;
using uploaddownloadfiles.Models;
using uploaddownloadfiles.Services;

namespace uploaddownloadfiles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var openapisecurityscheme = new NSwag.OpenApiSecurityScheme();
            openapisecurityscheme.Type = NSwag.OpenApiSecuritySchemeType.ApiKey;
            openapisecurityscheme.Name = "Authorization Token";
            openapisecurityscheme.Description = "Bearer + valid jwt token into field";
            openapisecurityscheme.In = NSwag.OpenApiSecurityApiKeyLocation.Header;

            services.AddSwaggerDocument(options =>
            {
                options.Title = "Kaizenblobservice";
                options.DocumentName = "KaizenUploadDowload V1";
                options.Description = "Kaizen upload and Dowload service internal Api";

                options.AddSecurity("Bearer", Enumerable.Empty<string>(),  openapisecurityscheme);
            }
            );
            services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorageConnectionString")));
            services.AddSingleton<IBlobService, BlobService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(app => app.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();
            });
        }
    }
}
