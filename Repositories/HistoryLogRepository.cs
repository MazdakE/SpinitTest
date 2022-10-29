using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpinitTest.Context;
using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Queries;
using SpinitTest.Repositories.Interfaces;

namespace SpinitTest.Repositories
{
    public class HistoryLogRepository : IHistoryLogRepository
    {
        private readonly DataContext Context;
        private IQueryable<HistoryLogEntity> DbSet;
        private readonly IMapper _mapper;
        public HistoryLogRepository(DataContext context, IMapper mapper)
        {
            Context = context;
            DbSet = Context.Set<HistoryLogEntity>().AsQueryable();
            _mapper = mapper;
        }
        public async Task<HistoryLogEntity> Create(StateQuery query)
        {
            var entity = _mapper.Map<HistoryLogEntity>(query);
            entity.SearchDate = DateTime.Now;
            await Context.Set<HistoryLogEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;

            //return _mapper.Map<StateModel>(await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id));
        }

        public async Task<List<HistoryLogEntity>> Get() => DbSet.ToList();

        // GET kvar, fixa en endpoint för det så ska det vara klart sen
    }
}
