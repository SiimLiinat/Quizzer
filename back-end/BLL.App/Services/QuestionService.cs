using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class QuestionService :
        BaseEntityService<IAppUnitOfWork, IQuestionRepository, BllAppDTO.Question, DALAppDTO.Question>, IQuestionService
    {
        public QuestionService(IAppUnitOfWork serviceUow, IQuestionRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new QuestionMapper(mapper))
        {
        }


        public async Task<IEnumerable<BllAppDTO.Question>> GetAllQuestions(Guid? quizId)
        {
            var questions = await ServiceUow.Questions.GetAllAsync();
            var res = questions.Select(m => Mapper.Map(m)).ToList()!;
            if (quizId != null) res = res.FindAll(g => g!.QuizId == quizId);

            return res!;
        }
    }
}