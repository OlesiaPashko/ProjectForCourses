using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using AutoMapper;
using CustomIdentityApp.Models;
using Project.Helpers;
using BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Project.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Project.Contracts.V1.Requests;
using Project.Contracts.V1.Responses;

namespace Project.Controllers
{
    //[Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ApplicationContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
       // private readonly IIdentityService _identityService;
        public AccountsController(UserManager<User> userManager, IMapper mapper, ApplicationContext appDbContext, JwtSettings jwtSettings)//, IIdentityService identityService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _jwtSettings = jwtSettings;
            //_identityService = identityService;
        }

        // POST api/accounts/register
        [HttpPost]
        [Route("api/account/register")]
        public async Task<IActionResult> Post([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_appDbContext.Users.Any(elem => elem.UserName == model.UserName))
                return BadRequest(new AuthentificationResult { 
                    Token = "", Errors = new IdentityError[] { new IdentityError() { Code = "DuplicateUserName", Description = "Username is already taken" } }, 
                    Success = false});
            var userIdentity = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) 
                return new BadRequestObjectResult(new AuthentificationResult {
                    Token = "", Errors = result.Errors, Success = false });
            await _userManager.AddToRoleAsync(userIdentity, "User");
            await _appDbContext.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var userRoles = await _userManager.GetRolesAsync(userIdentity);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email),
                    new Claim("id", userIdentity.Id),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new OkObjectResult(new AuthentificationResult{ Token = tokenHandler.WriteToken(token), Errors=result.Errors, Success=result.Succeeded });
        }

        // POST api/accounts/login
        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Post([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null) 
                return new BadRequestObjectResult("User does not exist");

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!userHasValidPassword)
                return new BadRequestObjectResult("Combination of login and password is wrong");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new OkObjectResult(new AuthentificationResult { Token = tokenHandler.WriteToken(token), Errors = null, Success = true });
        }
    }
}
