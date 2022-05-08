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
    public class EfPlayerRepository : IPlayerRepository
    {
        private readonly FootballDbContext dbContext;
        public EfPlayerRepository(FootballDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(Player entity)
        {
            await dbContext.Players.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var player = dbContext.Players.FirstOrDefault(p => p.Id == id);
            dbContext.Players.Remove(player);
            await dbContext.SaveChangesAsync();
        }

        public IList<Player> GetAllEntities()
        {
            var players = dbContext.Players.Include(p => p.Club).ToList();
            return players;
        }

        public async Task<IList<Player>> GetAllEntitiesAsync()
        {
            var players = await dbContext.Players.Include(p => p.Club).ToListAsync();
            return players;
        }

        public async Task<Player> GetEntityById(int id)
        {
            var player = await dbContext.Players.Include(p => p.Club).FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }

        public async Task<bool> IsExists(int id)
        {
            return await dbContext.Players.AnyAsync(p => p.Id == id);
        }

        public async Task<int> Update(Player entity)
        {
            dbContext.Players.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
