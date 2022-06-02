using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class AnswerMapper : BaseMapper<DAL.App.DTO.Answer, Domain.App.Answer>
    {
        public AnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}