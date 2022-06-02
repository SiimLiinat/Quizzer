using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizzerAnswerRepository : IBaseRepository<QuizzerAnswer>, IQuizzerAnswerRepositoryCustom<QuizzerAnswer>
    {
        
    }
    
    public interface IQuizzerAnswerRepositoryCustom<TEntity>
    {
    }
}