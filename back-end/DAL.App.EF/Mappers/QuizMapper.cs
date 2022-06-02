using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class QuizMapper : BaseMapper<DAL.App.DTO.Quiz, Domain.App.Quiz>
    {
        public QuizMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}