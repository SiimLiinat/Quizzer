using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionService : IBaseEntityService<BllAppDTO.Question, DALAppDTO.Question>, IQuestionRepositoryCustom<BllAppDTO.Question>
    {
        public Task<IEnumerable<BllAppDTO.Question>> GetAllQuestions(Guid? quizId);
    }
}