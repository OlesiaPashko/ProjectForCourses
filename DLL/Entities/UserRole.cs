using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Entities
{
    public class UserRole:IdentityRole
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
