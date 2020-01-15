using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
using BLL.Services;
using BLL.DTOs;

namespace Project.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagesController : Controller
    {

        private readonly IImageService _imageService;
        IHostingEnvironment _appEnvironment;
        private IUserService _userService;

        public ImagesController(IImageService imageService, IHostingEnvironment hostingEnvironment, IUserService userService)
        {
            _imageService = imageService;
            _appEnvironment = hostingEnvironment;
            _userService = userService;
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
            var caption = String.Format("{0}", Request.Form["ImageCaption"]);
            var userId = HttpContext.GetUserId();
            bool success = await _imageService.UploadImage(userId, file, new ImageDTO { Path = _appEnvironment.WebRootPath, Caption = caption });
            if(success)
            {
                return Ok("image was added");
            }
            else
            {
                return BadRequest("Image can not be added");
            }
        }

       /* [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.GetUserId();
            string login = (await _userService.GetUserByIdAsync(Guid.Parse(userId))).UserName;
            var images = await _imageService.getAllAsync();
            foreach(var item in images)
            {
                var image = System.IO.File.OpenRead("C:\\test\\random_image.jpeg");
                return File(image, "image/jpeg");
            }
            var image = System.IO.File.OpenRead("C:\\test\\random_image.jpeg");
            return File(image, "image/jpeg");
        }

        [HttpGet("api/images")]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.GetUserId();
            var images =await _imageService.getAllAsync();
            foreach(var image in images)
            {
                using (FileStream fstream = File.OpenRead(image.Path))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);

                }
                byte[] mas = File.ReadAllBytes(image.Path);
                string extention = Path.GetExtension(image.Path);
                string type = "png";
                if (extention == "jpg")
                    type = "jpeg";
                string file_type = "application/" + type;
                // Имя файла - необязательно
                string file_name = Path.GetFileName(path);
                return File(mas, file_type, file_name);
            }
            
            if (success)
            {
                return Ok("image was added");
            }
            else
            {
                return BadRequest("Image can not be added");
            }
        }*/
    }
}