using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using HOFORTaskPlanner.MockData;
using HOFORTaskPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HOFORTaskPlanner.Services
{
    public class UserService
    {
        private readonly List<User> _users;
        public List<User> PaginatedUsers { get; set; } = new List<User>();
        private DbGenericService<User> DbService { get; set; }

        public UserService(DbGenericService<User> dbService)
        {
            DbService = dbService;

            //_users = MockUsers.GetUsers();
            //InitializeDB();

            _users = DbService.GetObjectsAsync().Result.ToList();
        }

        public async Task InitializeDB()
        {
            foreach (var user in _users)
            {
                await DbService.AddObjectAsync(user);
            }
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


        public IEnumerable<User> FilterTeams(User.UserDepartments department)
        {
            var results = _users.Where(De => De.UserDepartment == department);
            if (results.Count() != 0)
            {
                return results;
            }
            return _users;
        }


        public List<User> GetUsersByDepartment(Models.User.UserDepartments userDepartment)
        {
            return _users.FindAll(user => user.UserDepartment == userDepartment);
        }
        public List<User> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = _users;
            var test = data.OrderBy(Us => Us.UserId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }

        public int GetCount()
        {
            return _users.Count;
        }

        public List<User> GetPaginated(List<User> users, int currentPage, int pageSize)
        {
            var test = new List<User>();
            foreach (var VARIABLE in users)
            {
                if (VARIABLE.UserRole != User.UserRoles.Leder)
                {
                    test.Add(VARIABLE);
                }
            }
            var data = test.OrderBy(Us => Us.UserId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            PaginatedUsers = test;

            return data;
        }

    }
}
