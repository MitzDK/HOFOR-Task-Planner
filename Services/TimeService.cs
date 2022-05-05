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
        private List<Time> _times;
        private DbGenericService<Time> DbService;

        public TimeService(DbGenericService<Time> dbService)
        {
            DbService = dbService;
            _times = MockTimes.GetTimes();
            //_times = DbService.GetObjectsAsync().Result.ToList();
        }

        public List<Time> GetTimes()
        {
            return _times;
        }
        public async Task AddTimeAsync(Time time)
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

        public List<Time> GetTimeByAssignmentId(int id)
        {
            if (GetTimes() != null)
            {
                var newList = GetTimes().FindAll(time => time.AssignmentId.Equals(id));
                return newList;
            }
            return null;
        }
        public Time GetTimeById(int id)
        {
            return GetTimes().Find(time => time.TimeId.Equals(id));
        }

        public async Task UpdateTimeAsync(Time time)
        {
            if (time != null)
            {
                await DbService.UpdateObjectAsync(time);
            }
        }

        public List<Time> GetTimeByYear(int year)
        {
            if (_times != null)
            {
                var newList = GetTimes().FindAll(time => time.Year.Equals(year));
                return newList;
            }
            return null;
        }

        public List<Time> GetTimeByYearAndAssignmentId(int year, int id)
        {

            if (GetTimes() != null)
            {
                var newList = GetTimes().FindAll(time => time.AssignmentId.Equals(id))
                    .FindAll(time => time.Year.Equals(year));
                return newList;
            }

            return null;
        }
    }
}
