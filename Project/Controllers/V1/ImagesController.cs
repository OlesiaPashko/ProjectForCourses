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
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Project.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagesController : Controller
    {

        private readonly ApplicationContext _DBcontext;
        IHostingEnvironment _appEnvironment;

        public ImagesController(ApplicationContext context, IHostingEnvironment hostingEnvironment)
        {
            _DBcontext = context;
            _appEnvironment = hostingEnvironment;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/UploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var file = Request.Form.Files.GetFile("Image");
            var userId = HttpContext.GetUserId();
            var login = _DBcontext.Users.FirstOrDefault(user => user.Id == userId).UserName;
            if (!Directory.Exists(_appEnvironment.WebRootPath + @"\Images\" + login))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + @"\Images\" + login);
            }
            //Upload Image
            var postedFile = file;
            //Create custom filename
            var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            string path = @"\Images\" + login + @"\" + imageName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            Image image = new Image { Path = path, Like = 0};
            _DBcontext.Images.Add(image);
            _DBcontext.SaveChanges();
            return Ok("image was added");
        }

        [HttpGet("api/images")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _DBcontext.Images.ToListAsync();
            return Ok(posts);
        }
    }
}