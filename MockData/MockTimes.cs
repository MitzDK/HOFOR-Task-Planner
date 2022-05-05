using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.MockData
{
    public class MockTimes
    {
        private static List<Time> _times = new List<Time>()
        {
            new Time(2022, Time.MonthName.Januar, 20, 1),
            new Time(2022, Time.MonthName.Februar, 20, 1),
            new Time(2022, Time.MonthName.Marts, 20, 1),
            new Time(2022, Time.MonthName.April, 20, 1),
            new Time(2022, Time.MonthName.Maj, 20, 1),
            new Time(2022, Time.MonthName.Juni, 20, 1),
            new Time(2022, Time.MonthName.Juli, 20, 1),
            new Time(2022, Time.MonthName.August, 20, 1),
            new Time(2022, Time.MonthName.September, 20, 1),
            new Time(2022, Time.MonthName.Oktober, 20, 1),
            new Time(2022, Time.MonthName.November, 20, 1),
            new Time(2022, Time.MonthName.December, 20, 1),
        };

        public static List<Time> GetTimes()
        {
            return _times;
        }
    }
}
