using System;
using System.Linq;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class QuizzerMapper
    {
        public static BLL.App.DTO.Quizzer MapToBll(QuizzerAdd quizzerAdd)
        {
            var res = new BLL.App.DTO.Quizzer
            {
                AppUserId = quizzerAdd.AppUserId,
                QuizId = quizzerAdd.QuizId,
                StartedAt = DateTime.Now,
                Points = 0
            };
            return res;
        }
        
        public static BLL.App.DTO.Quizzer MapToBll(Quizzer quizzer)
        {
            var res = new BLL.App.DTO.Quizzer
            {
                Id = quizzer.Id,
                AppUserId = quizzer.AppUserId,
                QuizId = quizzer.QuizId,
                StartedAt = DateTime.Parse(quizzer.StartedAt),
                FinishedAt = quizzer.FinishedAt != null ? DateTime.Parse(quizzer.FinishedAt) : null,
                Points = quizzer.Points
            };
            return res;
        }
        
        public static Quizzer MapToApi(BLL.App.DTO.Quizzer quizzer)
        {
            var res = new Quizzer
            {
                Id = quizzer.Id,
                AppUserId = quizzer.AppUserId,
                QuizId = quizzer.QuizId,
                StartedAt = quizzer.StartedAt.ToString("yyyy-MM-ddTHH:mm:ss"),
                FinishedAt = quizzer.FinishedAt?.ToString("HH:mm:ss dd-MM-yyyy"),
                Points = quizzer.Points,
                QuizTitle = quizzer.Quiz!.Title,
                AppUserName = quizzer.AppUser!.UserName,
                QuizPercentage = (float) quizzer.Points!.Value / quizzer.Quiz.Questions!.Where(q => q.Answers!.Count > 1).Sum(q => q.Points ?? 1) * 100
            };
            return res;
        }
    }
}