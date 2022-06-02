using System.Linq;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class AnswerMapper
    {
        public static BLL.App.DTO.Answer MapToBll(AnswerAdd answerAdd)
        {
            var res = new BLL.App.DTO.Answer
            {
                QuestionId = answerAdd.QuestionId,
                IsCorrect = answerAdd.IsCorrect,
                AnswerText = answerAdd.AnswerText,
                Url = answerAdd.Url,
            };
            return res;
        }
        
        public static BLL.App.DTO.Answer MapToBll(Answer answer)
        {
            var res = new BLL.App.DTO.Answer
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect,
                AnswerText = answer.AnswerText,
                Url = answer.Url
            };
            return res;
        }
        
        public static Answer MapToApi(BLL.App.DTO.Answer answer)
        {
            var res = new Answer
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect,
                AnswerText = answer.AnswerText,
                Url = answer.Url,
                Question = answer.Question!.QuestionText,
                Answerers = answer.QuizzerAnswers!.Select(q => q.Quizzer!.AppUser!.UserName).ToArray()
            };
            return res;
        }
    }
}