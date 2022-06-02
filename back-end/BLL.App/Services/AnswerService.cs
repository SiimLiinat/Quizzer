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
    public class AnswerService :
        BaseEntityService<IAppUnitOfWork, IAnswerRepository, BllAppDTO.Answer, DALAppDTO.Answer>, IAnswerService
    {
        public AnswerService(IAppUnitOfWork serviceUow, IAnswerRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new AnswerMapper(mapper))
        {
        }
    }
}