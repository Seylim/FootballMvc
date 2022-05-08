using Football.DataAccess.Data;
using Football.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.DataAccess.Repositories.Concrete
{
    public class EfLeagueRepository : ILeagueRepository
    {
        private readonly FootballDbContext dbContext;
        public EfLeagueRepository(FootballDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(League entity)
        {
            await dbContext.Leagues.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var league = dbContext.Leagues.FirstOrDefault(l => l.Id == id);
            dbContext.Remove(league);
            await dbContext.SaveChangesAsync();
        }

        public IList<League> GetAllEntities()
        {
            var leagues = dbContext.Leagues.Include(l => l.Clubs).ToList();
            return leagues;
        }

        public async Task<IList<League>> GetAllEntitiesAsync()
        {
            var leagues = await dbContext.Leagues.Include(l => l.Clubs).ToListAsync();
            return leagues;
        }

        public async Task<League> GetEntityById(int id)
        {
            var league = await dbContext.Leagues.Include(l => l.Clubs).FirstOrDefaultAsync(l => l.Id == id);
            return league;
        }

        public async Task<bool> IsExists(int id)
        {
            return await dbContext.Leagues.AnyAsync(l => l.Id == id);
        }

        public async Task<int> Update(League entity)
        {
            dbContext.Leagues.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
