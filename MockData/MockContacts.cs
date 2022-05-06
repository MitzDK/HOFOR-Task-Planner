using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.MockData
{
    public class MockContacts
    {
        private static List<Contact> _contacts = new List<Contact>()
        {
            new Contact("Frederik", "Rex", "20202020", "f@r.dk"),
            new Contact("Georg", "Tex", "21851929", "g@mail.dk"),
            new Contact("Trine", "Mex", "88794512", "o@utlook.dk")
        };

        public static List<Contact> GetMockContacts()
        {
            return _contacts;
        }
    }
}
