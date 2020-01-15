using BLL.DTOs;
using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService:IImageService
    {
        private IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> UploadImage(string userId, IFormFile file, ImageDTO imageDTO)
        {
            var login = (await _unitOfWork.Users.SingleOrDefaultAsync(user => user.Id == userId)).UserName;
            var rootPath = imageDTO.Path;
            if (!Directory.Exists(rootPath + @"\Images\" + login))
            {
                Directory.CreateDirectory(rootPath + @"\Images\" + login);
            }
            //Upload Image
            var postedFile = file;
            //Create custom filename
            var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            string path = @"\Images\" + login + @"\" + imageName;
            using (var fileStream = new FileStream(rootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            Image image = new Image { Path = path, Caption = imageDTO.Caption, Id = new Guid()};
            await _unitOfWork.Images.AddAsync(image);
            return (await _unitOfWork.CommitAsync()>0);
        }
    }
}
