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
    public class QuizzerAnswerService :
        BaseEntityService<IAppUnitOfWork, IQuizzerAnswerRepository, BllAppDTO.QuizzerAnswer, DALAppDTO.QuizzerAnswer>, IQuizzerAnswerService
    {
        public QuizzerAnswerService(IAppUnitOfWork serviceUow, IQuizzerAnswerRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new QuizzerAnswerMapper(mapper))
        {
        }
    }
}