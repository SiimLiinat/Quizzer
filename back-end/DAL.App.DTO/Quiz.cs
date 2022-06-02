using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Quiz : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        [MaxLength(200)] public string Title { get; set; } = default!;
        public char QuizOrPoll { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public int? TimeLimit { get; set; }
        public bool Repeatable { get; set; }
        public bool ShowAnswers { get; set; }
        
        public ICollection<Quizzer>? Quizzers { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}