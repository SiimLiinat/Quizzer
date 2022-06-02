using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class QuestionMapper : BaseMapper<DAL.App.DTO.Question, Domain.App.Question>
    {
        public QuestionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}