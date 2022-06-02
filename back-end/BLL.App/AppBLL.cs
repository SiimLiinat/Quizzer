using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }
        public IAppRoleService AppRoles => GetService<IAppRoleService>(() => new AppRoleService(Uow, Uow.AppRoles, Mapper));
        public IAppUserService AppUsers => GetService<IAppUserService>(() => new AppUserService(Uow, Uow.AppUsers, Mapper));
        public IAnswerService Answers => GetService<IAnswerService>(() => new AnswerService(Uow, Uow.Answers, Mapper));
        public IQuestionService Questions => GetService<IQuestionService>(() => new QuestionService(Uow, Uow.Questions, Mapper));
        public IQuizService Quizzes => GetService<IQuizService>(() => new QuizService(Uow, Uow.Quizzes, Mapper));
        public IQuizzerService Quizzers => GetService<IQuizzerService>(() => new QuizzerService(Uow, Uow.Quizzers, Mapper));
        public IQuizzerAnswerService QuizzerAnswers => GetService<IQuizzerAnswerService>(() => new QuizzerAnswerService(Uow, Uow.QuizzerAnswers, Mapper));
    }
}