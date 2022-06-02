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
    public class QuizService :
        BaseEntityService<IAppUnitOfWork, IQuizRepository, BllAppDTO.Quiz, DALAppDTO.Quiz>, IQuizService
    {
        public QuizService(IAppUnitOfWork serviceUow, IQuizRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new QuizMapper(mapper))
        {
        }
    }
}