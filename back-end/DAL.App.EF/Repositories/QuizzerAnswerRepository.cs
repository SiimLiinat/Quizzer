using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuizzerAnswerRepository : BaseRepository<DAL.App.DTO.QuizzerAnswer, QuizzerAnswer, AppDbContext>, IQuizzerAnswerRepository
    {
        public QuizzerAnswerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuizzerAnswerMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.QuizzerAnswer>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Answer)
                .Include(q => q.Quizzer)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.QuizzerAnswer?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(q => q.Answer)
                .Include(q => q.Quizzer);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}