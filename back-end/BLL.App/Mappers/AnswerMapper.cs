using AutoMapper;

namespace BLL.App.Mappers
{
    public class AnswerMapper : BaseMapper<BLL.App.DTO.Answer, DAL.App.DTO.Answer>
    {
        public AnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}