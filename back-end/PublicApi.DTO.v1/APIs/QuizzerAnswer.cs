using System;
using Domain.Base;

namespace PublicApi.DTO.v1.APIs
{
    public class QuizzerAnswer
    {
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; }
        public Guid QuizzerId { get; set; }
    }

    public class QuizzerAnswerAdd
    {
        public Guid AnswerId { get; set; }
        public Guid QuizzerId { get; set; }
    }
}