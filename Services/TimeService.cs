using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _times = DbService.GetObjectsAsync().Result.ToList();
        }

        public List<Time> GetTimes()
        {
            return _times;
        }
        public async Task AddUserAsync(Time time)
        {
            _times.Add(time);
            await DbService.AddObjectAsync(time);
        }

        public Time GetTimeById(int id)
        {
            return _times.Find(time => time.TimeId.Equals(id));
        }



        public async Task UpdateTimeAsync(Time time)
        {
            if (time != null)
            {
                await DbService.UpdateObjectAsync(time);
            }
        }
    }
}
