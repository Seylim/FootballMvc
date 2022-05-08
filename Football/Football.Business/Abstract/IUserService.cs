using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Abstract
{
    public interface IUserService
    {
        Task<ICollection<User>> GetAllAsync();
        IList<User> GetAll();
        Task<User> GetById(int id);
        Task<int> Add(User user);
        Task<int> Update(User user);
        Task Delete(int id);
        Task<bool> IsExists(int id);
        Task<User> ValidateUser(string userName, string password);
        Task<User> GetByUserName(string userName);
    }
}
