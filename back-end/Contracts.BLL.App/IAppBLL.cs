using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAppRoleService AppRoles { get;  }
        IAppUserService AppUsers { get;  }
        IAnswerService Answers { get;  }
        IQuestionService Questions { get;  }
        IQuizService Quizzes { get;  }
        IQuizzerService Quizzers { get;  }
        IQuizzerAnswerService QuizzerAnswers { get;  }
    }
}