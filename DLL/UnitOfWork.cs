using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Repositories;
using DLL.Repositories.Interfaces;

namespace DLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _db;
        private PostRepository _postRepository;
        private UserRepository _userRepository;
        private PhotoRepository _photoRepository;
        public IPhotoRepository Photos
        {
            get
            {
                if (_photoRepository == null)
                    _photoRepository = new PhotoRepository(_db);
                return _photoRepository;
            }
        }

        public IPostRepository Posts
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_db);
                return _postRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);
                return _userRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<int> CommitAsync()
        {
            return await _db.SaveChangesAsync();
        }

    }
}
