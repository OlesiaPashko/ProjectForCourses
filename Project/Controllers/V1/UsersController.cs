using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contracts.V1.Requests;
using Project.Models;
using Project.Services;

namespace Project.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        // GET: api/Posts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await _userService.GetUserByIdAsync(id);

            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            await _userService.CreateUserAsync(user);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var localUri = baseUrl + "/Api/Users/" + user.Id;
            return Created(localUri, user.Id);
        }

        // PUT: api/Posts/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserModel updatedUser)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var isUpdated = await _userService.UpdateUserAsync(user);
            if (isUpdated)
                return Ok(user);
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var isDeleted = await _userService.DeleteUserAsync(userId);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}