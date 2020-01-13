using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Models;
using Project.Services;

namespace Project.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IHostingEnvironment, Ho>();
            services.AddAuthorization();
            /*services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });*/
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });
        }
    }
}
