using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var file = Request.Form.Files["Image"];
            //Upload Image
            var postedFile = file;
            //Create custom filename
            var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            string path = "/Images/" + file.Name;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            Image image = new Image { Path = path, Like = 0 };
            _DBcontext.Images.Add(image);
            _DBcontext.SaveChanges();
            return Ok("image was added");
        }

        [HttpGet("api/images")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _DBcontext.Posts.ToListAsync();
            return Ok(posts);
        }
    }*/
}