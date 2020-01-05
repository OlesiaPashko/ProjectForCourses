using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contracts.V1.Requests;
using Project.Contracts.V1.Responses;
using Project.Extentions;
using Project.Models;
using Project.Models.V1.Requests;
using Project.Services;

namespace Project.Controllers.V1
{   
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: api/Posts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePostRequest createPostRequest)
        {
            var post = new Post { Name = createPostRequest.Name, UserId = HttpContext.GetUserId() };
            await _postService.CreatePostAsync(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var localUri = baseUrl + "/Api/Posts/" + post.Id;

            var response = new PostResponse { Id = post.Id };
            return Created(localUri, response);
        }

        // PUT: api/Posts/5
        [HttpPut("{postId}")]
        public async Task<IActionResult> Put(Guid postId, [FromBody] UpdatePostRequest request)
        {
            bool userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error="You don`t own this post"});
            }
            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = request.Name;
            var isUpdated = await _postService.UpdatePostAsync(post);
            if (isUpdated)
                return Ok(post);
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid postId)
        {
            bool userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You don`t own this post" });
            }
            var isDeleted = await _postService.DeletePostAsync(postId);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}
