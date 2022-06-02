using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Answer : DomainEntityId
    {
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
        
        public ICollection<QuizzerAnswer>? QuizzerAnswers { get; set; }
    }
}