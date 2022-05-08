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
    public class EfClubRepository : IClubRepository
    {
        private readonly FootballDbContext dbContext;
        public EfClubRepository(FootballDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(Club entity)
        {
            await dbContext.Clubs.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var club = dbContext.Clubs.FirstOrDefault(c => c.Id == id);
            dbContext.Clubs.Remove(club);
            await dbContext.SaveChangesAsync();
        }

        public IList<Club> GetAllEntities()
        {
            var clubs = dbContext.Clubs.Include(c => c.Coach).Include(c => c.League).Include(c => c.Players).ToList();
            return clubs;
        }

        public async Task<IList<Club>> GetAllEntitiesAsync()
        {
            var clubs = dbContext.Clubs.Include(c => c.Coach).Include(c => c.League).Include(c => c.Players).ToList();
            return clubs;
        }

        public async Task<Club> GetEntityById(int id)
        {
            var club = await dbContext.Clubs.Include(c => c.Coach).Include(c => c.League).Include(c => c.Players).FirstOrDefaultAsync(c => c.Id == id);
            return await dbContext.Clubs.FindAsync(id);
        }

        public async Task<bool> IsExists(int id)
        {
            return await dbContext.Clubs.AnyAsync(c => c.Id == id);
        }

        public async Task<int> Update(Club entity)
        {
            dbContext.Clubs.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
