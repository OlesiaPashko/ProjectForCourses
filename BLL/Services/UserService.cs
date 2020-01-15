using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService//:IUserService
    {
        /*private readonly ApplicationContext _DBcontext;

        public UserService(ApplicationContext context)
        {
            _DBcontext = context;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            await _DBcontext.Users.AddAsync(user);
            var createdCount = await _DBcontext.SaveChangesAsync();
            return createdCount > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            _DBcontext.Users.Remove(user);
            var deletedCount = await _DBcontext.SaveChangesAsync();
            return deletedCount > 0;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _DBcontext.Users.SingleOrDefaultAsync(x => x.Id == id.ToString());
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _DBcontext.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _DBcontext.Users.Update(user);
            var updatedCount = await _DBcontext.SaveChangesAsync();
            return updatedCount > 0;
        }*/
    }
}
