using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HOFORTaskPlanner.MockData
{
    public class MockUsers
    {
        private static List<User> _users = new List<User>()
        {
            new User("Martin", "Lylloff", "MLYL", "1234", "MLYL", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Digitalisering),
            new User("Victor", "Jejlskov", "VTOR", "1234", "VTOR", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Udvikling),
            new User("Andreas", "Broberg","ABRO", "1234", "ABRO", User.UserRoles.Azure, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Frederik", "Bressendorf", "FDOR", "1234", "FDOR", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Infrastruktur)
        };

        public static List<User> GetUsers()
        {
            return _users;
        }
    }

}
