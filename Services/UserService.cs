using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HOFORTaskPlanner.MockData;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class UserService
    {
        private readonly List<User> _users;
        private readonly DbGenericService<User> _dbService;

        public UserService(DbGenericService<User> dbService)
        {
            _dbService = dbService;
            _users = MockUsers.GetUsers();
            // _users = _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddUserAsync(User user)
        {
            _users.Add(user);
            await _dbService.AddObjectAsync(user);
        }

        public User GetUserById(int id)
        {
            return  _users.Find(Us => Us.UserId == id);
             
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user != null)
            {
                 await _dbService.UpdateObjectAsync(user);
            }
        }
        public async Task DeleteUserAsync(User user)
        {
            _users.Remove(user);
            await _dbService.DeleteObjectAsync(user);
        }

    }
}
