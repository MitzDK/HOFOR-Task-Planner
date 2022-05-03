using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HOFORTaskPlanner.Models
{
    public class Time
    {
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
