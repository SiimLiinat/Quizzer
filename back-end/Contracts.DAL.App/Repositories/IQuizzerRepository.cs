using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizzerRepository : IBaseRepository<Quizzer>, IQuizzerRepositoryCustom<Quizzer>
    {
        
    }
    
    public interface IQuizzerRepositoryCustom<TEntity>
    {
    }
}