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
            Admin,
            User
        }

        public enum UserRoles
        {
            Implementering,
            Tovholder,
            Projektleder,
            D365Platform,
            D365Ce,
            D365Fo,
            Udvikler,
            Koordinator,
            Bi,
            Web,
            Isdm,
            Løsningsdesign,
            Klient,
            Netværk,
            Server,
            Azure,
            Sikkerhed,
            Dba,
            Lcm,
            Support,
            Støttefunktion,
            Leder,
            Xprojektleder
        }

        public enum UserDepartments
        {
            Implementering,
            Udvikling,
            Infrastruktur,
            Digitalisering,
            Service
        }

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

        public Aktion(int userId, string username, string password, string firstName, string lastName, string displayName, UserTypes userType, UserRoles userRole, UserDepartments userDepartment, DateTime lastUpdated)
        {
            UserId = userId;
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
