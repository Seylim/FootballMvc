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
    public class EfUserRepository : IUserRepository
    {
        private readonly FootballDbContext dbContext;
        public EfUserRepository(FootballDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> FindByName(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> FindByNameAndByPassword(string userName, string password)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public IList<User> GetAllEntities()
        {
            return dbContext.Users.ToList();
        }

        public async Task<IList<User>> GetAllEntitiesAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User> GetEntityById(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<bool> IsExists(int id)
        {
            return await dbContext.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(User entity)
        {
            dbContext.Users.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
