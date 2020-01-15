using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DLL.Entities;
using DLL;
using BLL.Services;

namespace BLL.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IImageService, ImageService>();
           // services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });
        }

        /*public async Task MakeRoles(IServiceScope serviceScope)
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var admin = new IdentityRole("Admin");
            Console.WriteLine((await roleManager.CreateAsync(admin)).Succeeded);
            foreach (var error in ((await roleManager.CreateAsync(admin)).Errors))
            {
                Console.WriteLine($"ERROR CODE - {error.Code}");
                Console.WriteLine($"ERROR DESCRIPTION - {error.Description}");
            }

            var user = new IdentityRole("User");
                await roleManager.CreateAsync(user);
        }*/
    }
}
