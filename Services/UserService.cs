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
        private DbGenericService<User> DbService { get; set; }

        public UserService(DbGenericService<User> dbService)
        {
            DbService = dbService;

            //_users = MockUsers.GetUsers();
            //foreach (var user in _users)
            //{
            //    DbService.AddObjectAsync(user);
            //}

            _users = DbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddUserAsync(User user)
        {
            _users.Add(user);
            await DbService.AddObjectAsync(user);
        }

        public User GetUserById(int id)
        {
            return _users.Find(us => us.UserId.Equals(id));
        }

        public User GetUserByDisplayName(string displayName)
        {
            foreach (var user in _users)
            {
                if (!string.IsNullOrEmpty(displayName))
                {
                    if (user.DisplayName.ToLower().Equals(displayName.ToLower())) return user;
                }
            }
            return null;
        }
        public User GetUserByUsername(string username)
        {
            foreach (var user in _users)
            {
                if (user.UserName.ToLower().Equals(username))
                {
                    return user;
                }
            }

            return null;
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user != null)
            {
                await DbService.UpdateObjectAsync(user);
            }
        }
        public async Task DeleteUserAsync(User user)
        {
            _users.Remove(user);
            await DbService.DeleteObjectAsync(user);
        }

    }
}
