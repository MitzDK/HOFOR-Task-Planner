using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HOFORTaskPlanner.Models
{
    public class Contact
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Indtast fornavn")][Display(Name = "Fornavn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Indtast efternavn")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Indtast telefon nr")]
        [Display(Name = "Telefon Nr")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Indtast e-mail adresse")]
        [DataType(DataType.EmailAddress)][EmailAddress][Display(Name = "E-mail")]
        public string Email { get; set; }

        public Contact()
        {
            
        }

        public Contact(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
