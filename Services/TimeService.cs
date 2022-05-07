using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.MockData;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class TimeService
    {
        private List<TimeReg> _times;
        private DbGenericService<TimeReg> DbService;

        public TimeService(DbGenericService<TimeReg> dbService)
        {
            DbService = dbService;
            _times = MockTimes.GetTimes();
            //_times = DbService.GetObjectsAsync().Result.ToList();
        }

        public List<TimeReg> GetTimes()
        {
            return _times;
        }

        public async Task AddTimeAsync(TimeReg time)
        {
            _times.Add(time);
            await DbService.AddObjectAsync(time);
        }

        public int TotalTimeForAssignmentPlanned(int id)
        {
            if (GetTimeByAssignmentId(id) != null)
            {
                return GetTimeByAssignmentId(id).Sum(tim => tim.Hours);
            }

            return 0;
        }

        public List<TimeReg> GetTimeByAssignmentId(int id)
        {
            if (GetTimes() != null)
            {
                var newList = GetTimes().FindAll(time => time.AssignmentId.Equals(id));
                return newList;
            }

            return null;
        }

        public TimeReg GetTimeById(int id)
        {
            return GetTimes().Find(time => time.TimeId.Equals(id));
        }

        public async Task UpdateTimeAsync(TimeReg time)
        {
            if (time != null)
            {
                await DbService.UpdateObjectAsync(time);
            }
        }

        public List<TimeReg> GetTimeByYear(int year)
        {
            if (_times != null)
            {
                var newList = GetTimes().FindAll(time => time.Year.Equals(year));
                return newList;
            }

            return null;
        }

        public List<TimeReg> GetTimeByYearAndAssignmentId(int year, int id)
        {

            if (GetTimes() != null)
            {
                var newList = GetTimes().FindAll(time => time.AssignmentId.Equals(id))
                    .FindAll(time => time.Year.Equals(year));
                return newList;
            }

            return null;
        }
        public TimeReg GetTimeByYearAndMonthAndAssignmentId(int year, int month, int id)
        {
            if (_times.Exists(time =>
                time.AssignmentId == id && time.Year == year &&
                time.Month == (TimeReg.MonthName) month))
            {
                return _times.Find(time =>
                    time.AssignmentId == id && time.Year == year &&
                    time.Month == (TimeReg.MonthName) month);
            }
            return new TimeReg();
        }

        public bool IsCurrentMonth(int input)
        {
            if (Models.TimeReg.CurrentMonth() == (TimeReg.MonthName) input) return true;
            return false;
        }

        public async Task AddAndUpdateTimes(List<TimeReg> timeList)
        {
            _times = DbService.GetObjectsAsync().Result.ToList();
            foreach (var time in timeList)
            {
                if (_times.Exists(tim =>
                    tim.Year == time.Year && tim.Month == time.Month && tim.AssignmentId == time.AssignmentId))
                {
                    await DbService.UpdateObjectAsync(time);
                }
                else
                {
                    await DbService.AddObjectAsync(time);
                }
            }
        }
    }
}
