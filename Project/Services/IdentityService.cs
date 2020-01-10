using Microsoft.AspNetCore.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class IdentityService:IIdentityService
    {
        private UserManager<User> _userManager { get; set; }

        public IdentityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //public async Task<AuthentificationResult> RegisterAsync() 
        //{
        
        //}
    }
}
