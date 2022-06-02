using System;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class QuizzerAnswerMapper
    {
        public static BLL.App.DTO.QuizzerAnswer MapToBll(QuizzerAnswerAdd quizzerAnswerAdd)
        {
            var res = new BLL.App.DTO.QuizzerAnswer
            {
                AnswerId = quizzerAnswerAdd.AnswerId,
                QuizzerId = quizzerAnswerAdd.QuizzerId,
            };
            return res;
        }
        
        public static BLL.App.DTO.QuizzerAnswer MapToBll(QuizzerAnswer quizzerAnswer)
        {
            var res = new BLL.App.DTO.QuizzerAnswer
            {
                Id = quizzerAnswer.Id,
                AnswerId = quizzerAnswer.AnswerId,
                QuizzerId = quizzerAnswer.QuizzerId,
            };
            return res;
        }
        
        public static QuizzerAnswer MapToApi(BLL.App.DTO.QuizzerAnswer quizzerAnswer)
        {
            var res = new QuizzerAnswer
            {
                Id = quizzerAnswer.Id,
                AnswerId = quizzerAnswer.AnswerId,
                QuizzerId = quizzerAnswer.QuizzerId,
            };
            return res;
        }
    }
}