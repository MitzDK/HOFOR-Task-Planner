using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HOFORTaskPlanner.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
        private static List<User> _users = new List<User>()
        {
            new User("Martin", "Lylloff", "MLYL", _passwordHasher.HashPassword(null, "1234"), "MLYL", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Digitalisering),
            new User("Victor", "Jejlskov", "VTOR", _passwordHasher.HashPassword(null, "1234"), "VTOR", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Udvikling),
            new User("Andreas", "Broberg","ABRO", _passwordHasher.HashPassword(null, "1234"), "ABRO", User.UserRoles.Azure, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Frederik", "Bressendorf", "FDOR", _passwordHasher.HashPassword(null, "1234"), "FDOR", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Infrastruktur)
        };

        public static List<User> GetUsers()
        {
            _users[0].LastUpdated = DateTime.Now;
            _users[1].LastUpdated = DateTime.Now;
            _users[2].LastUpdated = DateTime.Now;
            _users[3].LastUpdated = DateTime.Now;
            return _users;
        }
    }

}
