using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1.APIs
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        [MaxLength(1000)] public string QuestionText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
        public int? Points { get; set; }
        public int? QuestionNumber { get; set; }
        
        // TotalQuestionCount?
        
        public string QuizTitle { get; set; } = default!;
        public bool HasCorrectAnswer { get; set; }
        public int TotalAnswers { get; set; }
        public int TotalQuizzerAnswers { get; set; }
    }

    public class QuestionAdd
    {
        public Guid QuizId { get; set; }
        [MaxLength(1000)] public string QuestionText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
        public int? Points { get; set; }
        public int? QuestionNumber { get; set; }
    }
}