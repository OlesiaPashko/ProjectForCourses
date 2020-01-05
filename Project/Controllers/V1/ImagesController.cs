using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Contracts.V1.Requests;
using Project.Extentions;
using Project.Models;

namespace Project.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly ApplicationContext _DBcontext;

        public ImagesController(ApplicationContext context)
        {
            _DBcontext = context;
        }

        [HttpPost("api/image")]
        public ActionResult Post([FromForm]RequestWithImage req)
        {
            Post post = new Post { Name = req.Name };//, UserId = HttpContext.GetUserId() };
            if (req.Image!=null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(req.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)req.Image.Length);
                }
                post.Image = imageData;
                _DBcontext.Posts.Add(post);
                _DBcontext.SaveChanges();
                return Ok(new { message = "Image Posted Successfully" });
            }
            else
            {
                return BadRequest("Image was added not correct");
            }
        }

        [HttpGet("api/images")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _DBcontext.Posts.ToListAsync();
            return Ok(posts);
        }
    }
}