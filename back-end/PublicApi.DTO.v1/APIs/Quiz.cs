using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1.APIs
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(200)] public string Title { get; set; } = default!;
        public char QuizOrPoll { get; set; } = default!;
        public string CreatedAt { get; set; } = default!;
        public int? TimeLimit { get; set; }
        public bool Repeatable { get; set; }
        public bool ShowAnswers { get; set; }
        
        public string AppUserName { get; set; } = default!;
        public int PointsSum { get; set; }
    }

    public class QuizAdd
    {
        public Guid AppUserId { get; set; }
        [MaxLength(200)] public string Title { get; set; } = default!;
        public char QuizOrPoll { get; set; } = default!;
        public DateTime CreatedAt = DateTime.Now;
        public int? TimeLimit { get; set; }
        public bool Repeatable { get; set; }
        public bool ShowAnswers { get; set; }
    }
}