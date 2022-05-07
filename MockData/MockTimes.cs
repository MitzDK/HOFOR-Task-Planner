using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.MockData
{
    public class MockTimes
    {
        private static List<TimeReg> _times = new List<TimeReg>()
        {
            new TimeReg(2022, TimeReg.MonthName.Januar, 20, 1),
            //new Time(2022, Time.MonthName.Februar, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.Marts, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.April, 20, 1),
            //new Time(2022, Time.MonthName.Maj, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.Juni, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.Juli, 20, 1),
            //new Time(2022, Time.MonthName.August, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.September, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.Oktober, 20, 1),
            //new Time(2022, Time.MonthName.November, 20, 1),
            new TimeReg(2022, TimeReg.MonthName.December, 20, 1),
        };

        public static List<TimeReg> GetTimes()
        {
            return _times;
        }
    }
}
