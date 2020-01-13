using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync();
        Task<bool> UpdateUserAsync(User user);
    }
}
