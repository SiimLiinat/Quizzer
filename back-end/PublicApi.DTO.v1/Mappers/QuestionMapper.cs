using System.Linq;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class QuestionMapper
    {
        public static BLL.App.DTO.Question MapToBll(QuestionAdd questionAdd)
        {
            var res = new BLL.App.DTO.Question
            {
                QuizId = questionAdd.QuizId,
                QuestionText = questionAdd.QuestionText,
                Url = questionAdd.Url,
                Points = questionAdd.Points,
                QuestionNumber = questionAdd.QuestionNumber,
            };
            return res;
        }
        
        public static BLL.App.DTO.Question MapToBll(Question question)
        {
            var res = new BLL.App.DTO.Question
            {
                Id = question.Id,
                QuizId = question.QuizId,
                QuestionText = question.QuestionText,
                Url = question.Url,
                Points = question.Points,
                QuestionNumber = question.QuestionNumber
            };
            return res;
        }
        
        public static Question MapToApi(BLL.App.DTO.Question question)
        {
            var res = new Question
            {
                Id = question.Id,
                QuizId = question.QuizId,
                QuestionText = question.QuestionText,
                Url = question.Url,
                Points = question.Points,
                QuestionNumber = question.QuestionNumber,
                QuizTitle = question.Quiz!.Title,
                HasCorrectAnswer = question.Answers!.Any(q => q.IsCorrect),
                TotalAnswers = question.Answers!.Count,
                TotalQuizzerAnswers = question.Answers!.Select(a => a.QuizzerAnswers!.Count).Sum()
            };
            return res;
        }
    }
}