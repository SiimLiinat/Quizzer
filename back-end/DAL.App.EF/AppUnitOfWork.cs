using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IAppRoleRepository AppRoles => GetRepository(() => new AppRoleRepository(UowDbContext, Mapper));
        public IAppUserRepository AppUsers => GetRepository(() => new AppUserRepository(UowDbContext, Mapper));
        public IAnswerRepository Answers => GetRepository(() => new AnswerRepository(UowDbContext, Mapper));
        public IQuestionRepository Questions => GetRepository(() => new QuestionRepository(UowDbContext, Mapper));
        public IQuizRepository Quizzes => GetRepository(() => new QuizRepository(UowDbContext, Mapper));
        public IQuizzerRepository Quizzers => GetRepository(() => new QuizzerRepository(UowDbContext, Mapper));
        public IQuizzerAnswerRepository QuizzerAnswers => GetRepository(() => new QuizzerAnswerRepository(UowDbContext, Mapper));
    }
}