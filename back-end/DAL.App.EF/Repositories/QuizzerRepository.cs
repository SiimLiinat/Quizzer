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
    public class QuizzerRepository : BaseRepository<DAL.App.DTO.Quizzer, Quizzer, AppDbContext>, IQuizzerRepository
    {
        public QuizzerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuizzerMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Quizzer>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.AppUser)
                .Include(q => q.Quiz)
                    .ThenInclude(q => q!.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(q => q.QuizzerAnswers)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Quizzer?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(q => q.AppUser)
                .Include(q => q.Quiz)
                    .ThenInclude(q => q!.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(q => q.QuizzerAnswers);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}