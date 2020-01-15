using BLL.Options;
using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class IdentityService:IIdentityService
    {
        private UserManager<User> _userManager { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }

        public IdentityService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync() 
        {
        
        }
    }
}
