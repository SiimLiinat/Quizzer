using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class QuizzerMapper : BaseMapper<DAL.App.DTO.Quizzer, Domain.App.Quizzer>
    {
        public QuizzerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}