using DLL.Entities;
using DLL.Repositories;
using DLL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public interface IUnitOfWork : IDisposable
    {
        IPhotoRepository Photos { get; }
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        Task<int> CommitAsync();
    }
}
