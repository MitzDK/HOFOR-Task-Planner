using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.VisualBasic;

namespace HOFORTaskPlanner.Models
{
    public class Time
    {
        public enum MonthName
        {
            Januar = 1,
            Februar = 2,
            Marts = 3,
            April = 4,
            Maj = 5,
            Juni = 6,
            Juli = 7,
            August = 8,
            September = 9,
            Oktober = 10,
            November = 11,
            December = 12
        }

        public static MonthName CurrentMonth()
        {
            return (MonthName) DateTime.Now.Month;
        }
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeId { get; set; }
        [Required][Range(2000, 3000)]
        public int Year { get; set; }
        [Required]
        public MonthName Month { get; set; }
        [Required]
        public int Hours { get; set; }
        [Required] public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        

        public Time()
        {
        }

        public Time(Assignment assignment)
        {
            Assignment = assignment;
        }
    }
}
