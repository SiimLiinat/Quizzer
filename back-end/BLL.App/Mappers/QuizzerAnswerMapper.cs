using AutoMapper;

namespace BLL.App.Mappers
{
    public class QuizzerAnswerMapper : BaseMapper<BLL.App.DTO.QuizzerAnswer, DAL.App.DTO.QuizzerAnswer>
    {
        public QuizzerAnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}