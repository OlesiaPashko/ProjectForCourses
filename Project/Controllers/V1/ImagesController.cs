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

        public ImagesController(IImageService imageService, IHostingEnvironment hostingEnvironment)
        {
            _imageService = imageService;
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

       /*( [HttpGet("api/images")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _DBcontext.Images.ToListAsync();
            return Ok(posts);
        }*/
    }
}