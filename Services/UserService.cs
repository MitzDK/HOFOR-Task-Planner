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
        //Bruges til at initialiserer databasen med data fra mockdata
        public async Task InitializeDB()
        {
            foreach (var user in _users)
            {
                await DbService.AddObjectAsync(user);
            }
        }
        //Tilføjer en User til database
        public async Task AddUserAsync(User user)
        {
            _users.Add(user);
            await DbService.AddObjectAsync(user);
        }
        //Returnerer en User med et givent Id
        public User GetUserById(int id)
        {
            return _users.Find(us => us.UserId.Equals(id));
        }
        //Returnerer en User med det givent DisplayName
        public User GetUserByDisplayName(string displayName)
        {
            return _users.Find(user => user.DisplayName.ToLower().Equals(displayName.ToLower()));
        }
        //Returnerer en User med det givne UserName
        public User GetUserByUsername(string username)
        {
            return _users.Find(user => user.UserName.ToLower().Equals(username.ToLower()));
        }

        //public IEnumerable<User> GetUsersByUserName(string username)
        //{
        //    if (string.IsNullOrEmpty(username)) return _users;
        //    var results = _users.FindAll(user => user.UserName.ToLower().Contains(username.ToLower()));
        //    if (results.Count == 0)
        //    {
        //        return _users;
        //    }

        //    return results;

        //}
        //Returnerer en collection af User-objekter, der opfylder søgekriteriet. Denne bruges i GetUsers søgefeltet.
        public IEnumerable<User> GetUsersBySearch(string search)
        {
            if (string.IsNullOrEmpty(search)) return _users;
            var results = _users.Where(user =>
                user.FirstName.ToLower().Contains(search.ToLower()) || user.LastName.ToLower().Contains(search.ToLower()) ||
                user.DisplayName.ToLower().Contains(search.ToLower()) || user.UserName.ToLower().Contains(search.ToLower()));
            if (results.Any())
            {
                return results;
            }

            return _users;
        }
        //Bruges i alle klasser, der har brug for en liste af Users. Dette gør at vi kan skifte fra data fra database til mockdata uden at skulle rette det alle steder,
        //der har brug for en liste af User-objekter
        public List<User> GetUsers()
        {

            return _users;
        }
        
        //Opdaterer User-objekt i database
        public async Task UpdateUserAsync(User user)
        {
            if (user != null)
            {
                await DbService.UpdateObjectAsync(user);
            }
        }
        //Sletter bruger fra database. Bruges ikke da vi bevarer data ved at arkivere det i stedet.
        public async Task DeleteUserAsync(User user)
        {
            _users.Remove(user);
            await DbService.DeleteObjectAsync(user);
        }

        //Filtrerer brugere efter enhed/department
        public IEnumerable<User> FilterTeams(User.UserDepartments department)
        {
            var results = _users.Where(De => De.UserDepartment == department);
            if (results.Count() != 0)
            {
                return results;
            }
            return _users;
        }
        //Henter alle brugere som er del af en enhed/department
        public List<User> GetUsersByDepartment(Models.User.UserDepartments userDepartment)
        {
            return _users.FindAll(user => user.UserDepartment == userDepartment);
        }

        //Bruger currentPage og pageSize til at vise hvor mange User-objekter der skal fremgå på hver side, og for at dele listen af assignment op i flere dele. 
        public List<User> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = _users;
            var test = data.OrderBy(Us => Us.UserId).Where(us=>us.UserType != User.UserTypes.Arkiveret).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        public List<User> GetPaginatedResultNoArchived(int currentPage, int pageSize = 10)
        {
            var data = _users;
            var test = data.OrderBy(Us => Us.UserId).Where(Us=>Us.UserType != User.UserTypes.Arkiveret).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        //Brugt til pagination i GetUsers-razorpage. Metoden tager en liste af Assignment-objekter og imod både en int, der repræsenterer den nuværende side af pagination og hvor mange objekter hver side skal bestå af som argumenter
        public List<User> GetPaginatedResultList(IEnumerable<User> userList, int currentPage, int pageSize)
        {
            var data = userList;
            var test = data.OrderBy(Us => Us.UserId).Where(User=>User.UserType != User.UserTypes.Arkiveret).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        //Antallet User-objekter
        public int GetCount()
        {
            return _users.Count;
        }
        //Filtrerer ledere fra GetUsers
        public List<User> GetPaginatedNoLeaderRole(List<User> users, int currentPage, int pageSize)
        {
            var data = users.Where(us => us.UserRole != User.UserRoles.Leder && us.UserType != Models.User.UserTypes.Arkiveret).ToList();
            data = data.OrderBy(Us => Us.UserId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            PaginatedUsers = data;

            return data;
        }
        public string UserDisplayName(int userId)
        {
            if (GetUserById(userId) != null)
            {
                return GetUserById(userId).DisplayName;
            }
            return "N/A";
        }


    }
}
