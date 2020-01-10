using DLL.Entities;
using DLL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DataContext context)
            : base(context)
        { }
    }
}
