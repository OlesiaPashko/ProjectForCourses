﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Entities
{
    public class Role:IdentityRole
    {
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}