using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByNameAndByPassword(string userName, string password);
        Task<User> FindByName(string userName);
    }
}
