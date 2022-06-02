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
    public class QuizRepository : BaseRepository<DAL.App.DTO.Quiz, Quiz, AppDbContext>, IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuizMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Quiz>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.AppUser)
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(q => q.Quizzers)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Quiz?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(q => q.AppUser)
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(q => q.Quizzers)
                .AsNoTracking();
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}