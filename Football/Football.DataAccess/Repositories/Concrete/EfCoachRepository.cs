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
    public class EfCoachRepository : ICoachRepository
    {
        private readonly FootballDbContext dbContext;
        public EfCoachRepository(FootballDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(Coach entity)
        {
            await dbContext.Coaches.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var coach = dbContext.Coaches.FirstOrDefault(c => c.Id == id);
            dbContext.Coaches.Remove(coach);
            await dbContext.SaveChangesAsync();
        }

        public IList<Coach> GetAllEntities()
        {
            return dbContext.Coaches.ToList();
        }

        public async Task<IList<Coach>> GetAllEntitiesAsync()
        {
            return await dbContext.Coaches.ToListAsync();
        }

        public async Task<Coach> GetEntityById(int id)
        {
            return await dbContext.Coaches.FindAsync(id);
        }

        public async Task<bool> IsExists(int id)
        {
            return await dbContext.Coaches.AnyAsync(c => c.Id == id);
        }

        public async Task<int> Update(Coach entity)
        {
            dbContext.Coaches.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
