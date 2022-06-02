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
    public class AnswerRepository : BaseRepository<DAL.App.DTO.Answer, Answer, AppDbContext>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AnswerMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Answer>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(a => a.Question)
                .Include(a => a.QuizzerAnswers)
                    .ThenInclude(q => q.Quizzer)
                    .ThenInclude(q => q!.AppUser)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Answer?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(a => a.Question)
                .Include(a => a.QuizzerAnswers)
                    .ThenInclude(q => q.Quizzer)
                        .ThenInclude(q => q!.AppUser);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}