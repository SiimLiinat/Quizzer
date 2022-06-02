using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1.APIs
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
        
        public string Question { get; set; } = default!;
        public string[] Answerers { get; set; } =  new List<string>().ToArray();
    }
    
    public class AnswerAdd
    {
        public Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; } = default!;
        [MaxLength(500)] public string? Url { get; set; }
    }
}