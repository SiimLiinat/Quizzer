﻿using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<AppRole> AppRoles { get; set; } = default!;
        public DbSet<AppUserRole> AppUserRoles { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; } = default!;
        
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<Quizzer> Quizzers { get; set; } = default!;
        public DbSet<QuizzerAnswer> QuizzerAnswers { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}