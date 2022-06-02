using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        public string? ProfilePicture { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<Quizzer>? Quizzers { get; set; }
        public ICollection<AppUserRole>? AppUserRoles { get; set; }
    }
}