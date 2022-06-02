using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Question : DomainEntityId
    {
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        [MaxLength(1000)] public string QuestionText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
        public int? Points { get; set; }
        public int? QuestionNumber { get; set; }
        
        public ICollection<Answer>? Answers { get; set; }
    }
}