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
    public class TimeReg
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
            return (MonthName)DateTime.Now.Month;
        }

        public static string AbrMonthName(int monthNo)
        {
            switch (monthNo)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "Maj";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Okt";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
            }
            return "Jan";
        }
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeId { get; set; }
        [Required][Range(2000, 3000)] 
        public int Year { get; set; }
        [Required] 
        public MonthName Month { get; set; }
        [Required] 
        public int Hours { get; set; }
        [Required] 
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        

        public TimeReg()
        {
        }

        public TimeReg(Assignment assignment)
        {
            Assignment = assignment;
        }

        public TimeReg(int year, MonthName month, int hours, int assignmentId)
        {
            Year = year;
            Month = month;
            Hours = hours;
            AssignmentId = assignmentId;
        }
    }
}
