using DLL.Entities;
using DLL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Image> Images { get; }
        IRepository<UserImage> LikesFromUserToImage { get; }
        //UserManager<User> UserManager { get; }
        //RoleManager<User> RoleManager { get; }
        Task<int> CommitAsync();
    }
}
