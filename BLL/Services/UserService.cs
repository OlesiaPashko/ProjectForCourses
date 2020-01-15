using AutoMapper;
using BLL.DTOs;
using DLL;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    /*public class UserService { }:IUserService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            var createdCount = await _unitOfWork.CommitAsync();
            return createdCount > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            _unitOfWork.Users.Remove(user);
            var deletedCount = await _unitOfWork.CommitAsync();
            return deletedCount > 0;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _unitOfWork.Users.SingleOrDefaultAsync(x => x.Id == id.ToString());
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return (await _unitOfWork.Users.GetAllAsync()).ToList();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var userToUpdate = await GetUserByIdAsync(Guid.Parse(user.Id));
            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.
            var updatedCount = await _unitOfWork.CommitAsync();
            return updatedCount > 0;
        }*/
  //  }

}
