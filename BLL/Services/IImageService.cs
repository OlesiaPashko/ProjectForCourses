using BLL.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IImageService
    {
        Task<bool> UploadImage(string userId, IFormFile file, ImageDTO imageDTO);

        //Task<List<ImageDTO>> getAllAsync();

        //Task<IFormFile> getFIleAsync();

    }
}
