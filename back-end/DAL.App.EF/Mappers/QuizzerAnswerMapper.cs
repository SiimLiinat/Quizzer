using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class QuizzerAnswerMapper : BaseMapper<DAL.App.DTO.QuizzerAnswer, Domain.App.QuizzerAnswer>
    {
        public QuizzerAnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}