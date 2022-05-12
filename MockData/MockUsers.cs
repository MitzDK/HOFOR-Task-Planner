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
            new User("Andreas", "Broberg","ABRO", _passwordHasher.HashPassword(null, "1234"), "ABRO", User.UserRoles.Azure, User.UserTypes.Admin, User.UserDepartments.Implementering),
            new User("Frederik", "Bressendorff", "FDOR", _passwordHasher.HashPassword(null, "1234"), "FDOR", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Infrastruktur),

            //Ledere
            new User("Per", "Efternavn", "PER", _passwordHasher.HashPassword(null, "1234"), "PER", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Infrastruktur),
            new User("Grube", "Efternavn", "GRUBE", _passwordHasher.HashPassword(null, "1234"), "GRUBE", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Digitalisering),
            new User("Troels", "Efternavn", "TROELS", _passwordHasher.HashPassword(null, "1234"), "TROELS", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Implementering),
            new User("Janika", "Gilbert", "JANIKA", _passwordHasher.HashPassword(null, "1234"), "JANIKA", User.UserRoles.Leder, User.UserTypes.Admin, User.UserDepartments.Udvikling),

            
            //Infrastruktur
            new User("Brian", "Efternavn", "BRIAN", _passwordHasher.HashPassword(null, "1234"), "BRIAN", User.UserRoles.Løsningsdesign, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Claus", "Efternavn", "CLAUS", _passwordHasher.HashPassword(null, "1234"), "CLAUS", User.UserRoles.Løsningsdesign, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Camilla", "Efternavn", "CAMILLA", _passwordHasher.HashPassword(null, "1234"), "CAMILLA", User.UserRoles.Isdm, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Finn", "Efternavn", "FINN", _passwordHasher.HashPassword(null, "1234"), "FINN", User.UserRoles.Server, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Flemming", "Efternavn", "FLEMMING", _passwordHasher.HashPassword(null, "1234"), "FLEMMING", User.UserRoles.Netværk, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Henrik", "Efternavn", "HENRIK", _passwordHasher.HashPassword(null, "1234"), "HENRIK", User.UserRoles.Azure, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Jens", "Efternavn", "JENS", _passwordHasher.HashPassword(null, "1234"), "JENS", User.UserRoles.Sikkerhed, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Jesper", "Efternavn", "JESPER", _passwordHasher.HashPassword(null, "1234"), "JESPER", User.UserRoles.Server, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Kiss", "Efternavn", "KISS", _passwordHasher.HashPassword(null, "1234"), "KISS", User.UserRoles.Isdm, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Lars", "Efternavn", "LARS", _passwordHasher.HashPassword(null, "1234"), "LARS", User.UserRoles.Server, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Lucas", "Efternavn", "LUCAS", _passwordHasher.HashPassword(null, "1234"), "LUCAS", User.UserRoles.Netværk, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Mehmet", "Efternavn", "MEHMET", _passwordHasher.HashPassword(null, "1234"), "MEHMET", User.UserRoles.Server, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Ray", "Efternavn", "RAY", _passwordHasher.HashPassword(null, "1234"), "RAY", User.UserRoles.Dba, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Roth", "Efternavn", "ROTH", _passwordHasher.HashPassword(null, "1234"), "ROTH", User.UserRoles.Netværk, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Sønderkær", "Efternavn", "SØNDERKÆR", _passwordHasher.HashPassword(null, "1234"), "SØNDERKÆR", User.UserRoles.Azure, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            new User("Strøbæk", "Efternavn", "STRØBÆK", _passwordHasher.HashPassword(null, "1234"), "STRØBÆK", User.UserRoles.Klient, User.UserTypes.User, User.UserDepartments.Infrastruktur),
            

            //Digitalisering
            new User("Kari", "Efternavn", "KARI", _passwordHasher.HashPassword(null, "1234"), "KARI", User.UserRoles.Lcm, User.UserTypes.User, User.UserDepartments.Digitalisering),
            new User("Lisbeth", "Efternavn", "LISBETH", _passwordHasher.HashPassword(null, "1234"), "LISBETH", User.UserRoles.Lcm, User.UserTypes.User, User.UserDepartments.Digitalisering),
            new User("Michael", "Efternavn", "MICHAEL", _passwordHasher.HashPassword(null, "1234"), "MICHAEL", User.UserRoles.Lcm, User.UserTypes.User, User.UserDepartments.Digitalisering),
            new User("Søren", "Efternavn", "SØREN", _passwordHasher.HashPassword(null, "1234"), "SØREN", User.UserRoles.Lcm, User.UserTypes.User, User.UserDepartments.Digitalisering),
            

            //Implementering
            new User("Emma", "J", "EMMAJ", _passwordHasher.HashPassword(null, "1234"), "EMMAJ", User.UserRoles.Projektleder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Henning", "Efternavn", "HENNING", _passwordHasher.HashPassword(null, "1234"), "HENNING", User.UserRoles.Tovholder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Franz", "Efternavn", "FRANZ", _passwordHasher.HashPassword(null, "1234"), "FRANZ", User.UserRoles.Projektleder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Kristian", "Efternavn", "KRISTIAN", _passwordHasher.HashPassword(null, "1234"), "KRISTIAN", User.UserRoles.Implementering, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Kåre", "Efternavn", "KÅRE", _passwordHasher.HashPassword(null, "1234"), "KÅRE", User.UserRoles.Tovholder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Laura", "Efternavn", "LAURA", _passwordHasher.HashPassword(null, "1234"), "LAURA", User.UserRoles.Projektleder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Mette", "Efternavn", "METTE", _passwordHasher.HashPassword(null, "1234"), "METTE", User.UserRoles.Projektleder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Mikael", "Efternavn", "MIKAEL", _passwordHasher.HashPassword(null, "1234"), "MIKAEL", User.UserRoles.Projektleder, User.UserTypes.User, User.UserDepartments.Implementering),
            new User("Sandra", "Efternavn", "SANDRA", _passwordHasher.HashPassword(null, "1234"), "SANDRA", User.UserRoles.Implementering, User.UserTypes.User, User.UserDepartments.Implementering),

            //Udvikling
            new User("Alexandra", "Efternavn", "ALEXANDRA", _passwordHasher.HashPassword(null, "1234"), "ALEXANDRA", User.UserRoles.Koordinator, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Esben", "Efternavn", "ESBEN", _passwordHasher.HashPassword(null, "1234"), "ESBEN", User.UserRoles.Udvikler, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Joakim", "Efternavn", "JOAKIM", _passwordHasher.HashPassword(null, "1234"), "JOAKIM", User.UserRoles.Bi, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Jørgen", "Efternavn", "JØRGEN", _passwordHasher.HashPassword(null, "1234"), "JØRGEN", User.UserRoles.Udvikler, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Martin", "Efternavn", "MARTIN", _passwordHasher.HashPassword(null, "1234"), "MARTIN", User.UserRoles.Web, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Peder", "Efternavn", "PEDER", _passwordHasher.HashPassword(null, "1234"), "PEDER", User.UserRoles.D365Fo, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Mia", "Efternavn", "MIA", _passwordHasher.HashPassword(null, "1234"), "MIA", User.UserRoles.D365Ce, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Svend", "Efternavn", "SVEND", _passwordHasher.HashPassword(null, "1234"), "SVEND", User.UserRoles.D365Platform, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Platform", " ", "PLATFORM", _passwordHasher.HashPassword(null, "1234"), "PLATFORM", User.UserRoles.D365Platform, User.UserTypes.User, User.UserDepartments.Udvikling),
            new User("Ulrich", " ", "ULRICH", _passwordHasher.HashPassword(null, "1234"), "ULRICH", User.UserRoles.Bi, User.UserTypes.User, User.UserDepartments.Udvikling),
        };

        public static List<User> GetUsers()
        {
            foreach (var user in _users)
            {
                user.LastUpdated = DateTime.Now;
            }
            return _users;
        }
    }
}
