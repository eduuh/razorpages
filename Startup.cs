using System.Linq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UploadandDowloadService.Interface;
using UploadandDowloadService.Models;
using UploadandDowloadService.Infratructure;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using UploadandDowloadService.Areas.Identity;
using uploaddownloadfiles.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using NSwag;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UploadandDowloadService
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


            //configure mys sql connection
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                Configuration.GetConnectionString("AzureSqlServiceConnectionString"),
                   o => o.EnableRetryOnFailure()
                );
                options.UseLazyLoadingProxies();
            });


            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });


            services.AddIdentityCore<AppUser>()
             .AddRoles<IdentityRole>()
             .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<AppUser, IdentityRole>>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/SignIn";
                options.AccessDeniedPath = "Identity/SignIn";
            });

            //  services.AddRazorPages();

            services.AddCors();
            services.AddAutoMapper(typeof(Startup));


            services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorageConnectionString")));
            services.AddSingleton<IBlobService, BlobService>();
            services.AddScoped<IJwtToken, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            // diffeernt instance
            services.AddTransient<IUser, UploadandDowloadService.Infratructure.User>();



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokenkey"]));
            var TokenValidationParameter = new TokenValidationParameters();
            TokenValidationParameter.ValidateIssuerSigningKey = true;
            TokenValidationParameter.IssuerSigningKey = key;
            TokenValidationParameter.ValidateAudience = false;
            TokenValidationParameter.ValidateIssuer = false;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,

                };
            }).AddCookie(IdentityConstants.ApplicationScheme,
            o =>
            {
                o.Cookie.Expiration = TimeSpan.FromHours(8);
                o.Cookie.SameSite = SameSiteMode.Strict;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                o.AccessDeniedPath = new PathString("/");
                o.ExpireTimeSpan = TimeSpan.FromHours(8);
                o.LoginPath = new PathString("/sign-in");
                o.LogoutPath = new PathString("/sign-out");
                o.SlidingExpiration = true;
            });

            services.AddSwaggerDocument(options =>
            {
                options.Title = "Kaizenblobservice";
                options.DocumentName = "KaizenUploadDowload V1";
                options.Description = "Kaizen upload and Dowload service internal Api";
                options.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Bearer + valid jwt token into field",
                    In = OpenApiSecurityApiKeyLocation.Header
                });
            }
            );

            // services.AddHostedService<SeedDataHostedService>();
            //services.AddRazorPages();
            services.AddControllersWithViews();
            //    services.AddAuthorization(options => options.AddPolicy("Admin", builder =>
            //     {
            //         builder.RequireAssertion(async context =>
            //         {
            //             string[] roles = { };
            //             if (context.Resource is AuthorizationFilterContext mvc)
            //             {
            //                 var username = mvc.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //                 var usermanager = mvc.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();

            //                 var user = await usermanager.FindByNameAsync(username);
            //                 roles = (await usermanager.GetRolesAsync(user)).ToArray();
            //                 return roles.Contains("Admin");
            //             }
            //             return roles.Contains("Admin");

            //         });
            //     }));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            // app.UseSwaggerUi3();       

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseDefaultFiles();
            app.UseCors(app => app.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
