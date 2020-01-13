using DLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Identity
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        public ApplicationRoleManager(RoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer lookupNormalizer, 
            IdentityErrorDescriber identityErrorDescriber, ILogger<RoleManager<Role>> logger)
                    : base(store, roleValidators, lookupNormalizer, identityErrorDescriber, logger)
        { }
    }
}
