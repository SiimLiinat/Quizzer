using System;
using System.Linq;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class QuizMapper
    {
        public static BLL.App.DTO.Quiz MapToBll(QuizAdd quizAdd)
        {
            var res = new BLL.App.DTO.Quiz
            {
                AppUserId = quizAdd.AppUserId,
                Title = quizAdd.Title,
                QuizOrPoll = quizAdd.QuizOrPoll,
                CreatedAt = DateTime.Now,
                TimeLimit = quizAdd.TimeLimit * 60,
                Repeatable = quizAdd.Repeatable
            };
            return res;
        }
        
        public static BLL.App.DTO.Quiz MapToBll(Quiz quiz)
        {
            var res = new BLL.App.DTO.Quiz
            {
                Id = quiz.Id,
                AppUserId = quiz.AppUserId,
                Title = quiz.Title,
                QuizOrPoll = quiz.QuizOrPoll,
                CreatedAt = DateTime.Parse(quiz.CreatedAt),
                TimeLimit = quiz.TimeLimit * 60,
                Repeatable = quiz.Repeatable,
                ShowAnswers = quiz.ShowAnswers
            };
            return res;
        }
        
        public static Quiz MapToApi(BLL.App.DTO.Quiz quiz)
        {
            var res = new Quiz
            {
                Id = quiz.Id,
                AppUserId = quiz.AppUserId,
                Title = quiz.Title,
                QuizOrPoll = quiz.QuizOrPoll,
                CreatedAt = quiz.CreatedAt.ToString("HH:mm dd-MM-yyyy"),
                TimeLimit = quiz.TimeLimit / 60,
                AppUserName = quiz.AppUser!.UserName,
                Repeatable = quiz.Repeatable,
                ShowAnswers = quiz.ShowAnswers,
                PointsSum = quiz.Questions!.Where(q => q.Answers!.Count > 1).Sum(q => q.Points ?? 1)
            };
            return res;
        }
    }
}