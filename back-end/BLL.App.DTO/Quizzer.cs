using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Quizzer : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int? Points { get; set; }
        
        public ICollection<QuizzerAnswer>? QuizzerAnswers { get; set; }
    }
}