using AutoMapper;

namespace BLL.App.Mappers
{
    public class QuizzerMapper : BaseMapper<BLL.App.DTO.Quizzer, DAL.App.DTO.Quizzer>
    {
        public QuizzerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}