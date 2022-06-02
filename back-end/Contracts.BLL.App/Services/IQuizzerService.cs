using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IQuizzerService : IBaseEntityService<BllAppDTO.Quizzer, DALAppDTO.Quizzer>, IQuizzerRepositoryCustom<BllAppDTO.Quizzer>
    {
        public Task<IEnumerable<BllAppDTO.Quizzer>> GetAllQuizzers(Guid? userId);
    }
}