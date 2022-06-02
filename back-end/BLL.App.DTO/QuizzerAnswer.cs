using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class QuizzerAnswer : DomainEntityId
    {
        public Guid AnswerId { get; set; }
        public Answer? Answer { get; set; }
        public Guid QuizzerId { get; set; }
        public Quizzer? Quizzer { get; set; }
    }
}