using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HOFORTaskPlanner.Models
{
    public class Aktion
    {
        public enum UserTypes
        {
            Admin = 1,
            User = 2
        }

        public enum UserRoles
        {
            Implementering = 1,
            Tovholder = 2,
            Projektleder = 3,
            D365Platform = 4,
            D365Ce = 5,
            D365Fo = 6,
            Udvikler = 7,
            Koordinator = 8,
            Bi = 9,
            Web = 10,
            Isdm = 11,
            Løsningsdesign = 12,
            Klient = 13,
            Netværk = 14,
            Server = 15,
            Azure = 16,
            Sikkerhed = 17,
            Dba = 18,
            Lcm = 19,
            Support = 20,
            Støttefunktion = 21,
            Leder = 22,
            Xprojektleder = 23
        }

        public enum UserDepartments
        {
            Implementering = 1,
            Udvikling = 2,
            Infrastruktur = 3,
            Digitalisering = 4,
            Service = 5
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required] 
        public string Username { get; set; }
        [Required][DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public UserTypes UserType { get; set; }
        [Required]
        public UserRoles UserRole { get; set; }
        [Required]
        public UserDepartments UserDepartment { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }

        public Aktion()
        {
        }

        public Aktion(string username, string password, string firstName, string lastName, string displayName, UserTypes userType, UserRoles userRole, UserDepartments userDepartment, DateTime lastUpdated)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            UserType = userType;
            UserRole = userRole;
            UserDepartment = userDepartment;
            LastUpdated = lastUpdated;
        }
    }
}
