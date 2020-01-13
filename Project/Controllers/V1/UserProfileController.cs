using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Extentions;
using Project.Services;

namespace Project.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }
        //Get : "api/UserProfile"
        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            var UserId = HttpContext.GetUserId();
            var user = await _userService.GetUserByIdAsync(Guid.Parse(UserId));
            return new
            {
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName
            };
        }
    }
}