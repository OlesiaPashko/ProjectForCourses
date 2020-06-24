using AutoMapper;
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
        private IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> getAllAsync()
        {
            return _mapper.Map< List<Image> , List<ImageDTO>>(await _unitOfWork.Images.GetAllAsync());   
        }

        /*public File getFIleAsync(string path)
        {
            byte[] mas = File.ReadAllBytes(path);
            string extention = Path.GetExtension(path);
            string type = "png";
            if (extention == "jpg")
                type = "jpeg";
            string file_type = "application/" + type;
            // Имя файла - необязательно
            string file_name = Path.GetFileName(path);
            return File(mas, file_type, file_name);
        }*/
        public async Task<bool> UploadImage(string userId, IFormFile file, ImageDTO imageDTO)
        {
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var login = user.UserName;
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
            await _unitOfWork.LikesFromUserToImage.AddAsync(new UserImage { Image = image, User = user });
            return (await _unitOfWork.CommitAsync()>0);
        }
        /*
        Task<List<ImageDTO>> IImageService.getAllAsync()
        {
            throw new NotImplementedException();
        }*/
    }
}
