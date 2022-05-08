using Football.Business.Abstract;
using Football.DataAccess.Repositories;
using BC = BCrypt.Net.BCrypt;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> Add(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            user.Role = "Client";
            return await userRepository.Add(user);
        }

        public async Task Delete(int id)
        {
            await userRepository.Delete(id);
        }

        public IList<User> GetAll()
        {
            return userRepository.GetAllEntities();
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await userRepository.GetAllEntitiesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await userRepository.GetEntityById(id);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await userRepository.FindByName(userName);
        }

        public async Task<bool> IsExists(int id)
        {
            return await userRepository.IsExists(id);
        }

        public async Task<int> Update(User user)
        {
            return await userRepository.Update(user);
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            var user = await userRepository.FindByName(userName);
            if (BC.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }
    }
}
