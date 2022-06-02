using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        [MaxLength(500)] public string? ProfilePicture { get; set; }
        
        public ICollection<Quiz>? Quizzes { get; set; }
        public ICollection<Quizzer>? Quizzers { get; set; }
        public ICollection<AppUserRole>? AppUserRoles { get; set; }
    }
}