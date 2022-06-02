using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            
            CreateMap<Answer, DAL.App.DTO.Answer>().ReverseMap();
            CreateMap<Question, DAL.App.DTO.Question>().ReverseMap();
            CreateMap<Quiz, DAL.App.DTO.Quiz>().ReverseMap();
            CreateMap<Quizzer, DAL.App.DTO.Quizzer>().ReverseMap();
            CreateMap<QuizzerAnswer, DAL.App.DTO.QuizzerAnswer>().ReverseMap();
        }
    }
}