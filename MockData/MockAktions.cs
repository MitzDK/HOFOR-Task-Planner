using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.MockData
{
    public class MockAktions
    {
        private static List<Aktion> _aktions = new List<Aktion>()
        {
            new Aktion("Testuser", "123", "John", "Johnson", "JOHN", Aktion.UserTypes.Admin, Aktion.UserRoles.Leder, Aktion.UserDepartments.Udvikling, DateTime.Now)
        };

        public static List<Aktion> GetAktions()
        {
            return _aktions;
        }
    }
}
