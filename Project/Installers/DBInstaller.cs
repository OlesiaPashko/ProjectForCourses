using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Models;

namespace Project.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
