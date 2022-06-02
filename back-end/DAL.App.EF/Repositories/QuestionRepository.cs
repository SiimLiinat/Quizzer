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
    public class QuestionRepository : BaseRepository<DAL.App.DTO.Question, Question, AppDbContext>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuestionMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Question>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.QuizzerAnswers)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Question?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.QuizzerAnswers);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}