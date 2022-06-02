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
    public class QuizzerService :
        BaseEntityService<IAppUnitOfWork, IQuizzerRepository, BllAppDTO.Quizzer, DALAppDTO.Quizzer>, IQuizzerService
    {
        public QuizzerService(IAppUnitOfWork serviceUow, IQuizzerRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new QuizzerMapper(mapper))
        {
        }

        public async Task<IEnumerable<BllAppDTO.Quizzer>> GetAllQuizzers(Guid? userId)
        {
            var quizzers = await ServiceUow.Quizzers.GetAllAsync();
            var res = quizzers.Select(m => Mapper.Map(m)).ToList()!;
            if (userId != null) res = res.FindAll(g => g!.AppUserId == userId.Value!);

            return res!;
        }
    }
}