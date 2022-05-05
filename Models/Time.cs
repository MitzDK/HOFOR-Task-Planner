using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public int Year { get; set; }
        public int Month { get; set; }

        public int Hours { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
        

        public Time()
        {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
        }

        public Time(User user, Assignment assignment)
        {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            User = user;
            Assignment = assignment;
        }
    }
}
