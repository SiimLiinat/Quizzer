using System;
using Domain.Base;

namespace PublicApi.DTO.v1.APIs
{
    public class Quizzer
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid QuizId { get; set; }
        public string StartedAt { get; set; } = default!;
        public string? FinishedAt { get; set; }
        public int? Points { get; set; }
        
        public string QuizTitle { get; set; } = default!;
        public string AppUserName { get; set; } = default!;
        public float QuizPercentage { get; set; } = default!;
    }

    public class QuizzerAdd
    {
        public Guid AppUserId { get; set; }
        public Guid QuizId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public int? Points { get; set; }
    }
}