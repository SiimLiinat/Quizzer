using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAppRoleRepository AppRoles { get;  }
        IAppUserRepository AppUsers { get;  }
        IAnswerRepository Answers { get;  }
        IQuestionRepository Questions { get;  }
        IQuizRepository Quizzes { get;  }
        IQuizzerRepository Quizzers { get;  }
        IQuizzerAnswerRepository QuizzerAnswers { get;  }
    }
}