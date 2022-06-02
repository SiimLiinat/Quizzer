using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            CreateMap<Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            
            CreateMap<Answer, Domain.App.Answer>().ReverseMap();
            CreateMap<Question, Domain.App.Question>().ReverseMap();
            CreateMap<Quiz, Domain.App.Quiz>().ReverseMap();
            CreateMap<Quizzer, Domain.App.Quizzer>().ReverseMap();
            CreateMap<QuizzerAnswer, Domain.App.QuizzerAnswer>().ReverseMap();
        }
    }
}