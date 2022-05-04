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
        [Required][Display(Name = "Fornavn")]
        public string FirstName { get; set; }
        [Required][Display(Name = "Efternavn")]
        public string LastName { get; set; }
        [Required][Display(Name = "Telefon Nr")]
        public string PhoneNumber { get; set; }
        [Required][DataType(DataType.EmailAddress)][EmailAddress][Display(Name = "E-mail")]
        public string Email { get; set; }



    }
}
