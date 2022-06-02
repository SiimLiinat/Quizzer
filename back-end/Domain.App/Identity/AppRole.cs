using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId
    {
        public ICollection<AppUserRole>? AppUserRoles { get; set; }
    }
}