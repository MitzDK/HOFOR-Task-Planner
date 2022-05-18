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

            //_times = MockTimes.GetTimes();
            //InitializeDB();

            _times = DbService.GetObjectsAsync().Result.ToList();
        }
        //Initiliserer database med data. Det kunne eksempelvis være mockdata instanseret i kontruktøren.
        public async Task InitializeDB()
        {
            foreach (var timeReg in _times)
            {
                await DbService.AddObjectAsync(timeReg);
            }
        }
        //Returnerer liste af TimeReg-objekter
        public List<TimeReg> GetTimes()
        {
            return _times;
        }

        //tilføjer TimeReg objekt til database
        public async Task AddTimeAsync(TimeReg time)
        {
            _times.Add(time);
            await DbService.AddObjectAsync(time);
        }
        //Bruges til at finde summen af alle TimeReg-objekter med et givent AssignmentId
        public int TotalTimeForAssignmentPlanned(int id)
        {
            if (GetTimeByAssignmentId(id) != null)
            {
                return GetTimeByAssignmentId(id).Sum(tim => tim.Hours);
            }

            return 0;
        }
        //Finder alle TimeReg-objekter med et givent AssignmentId
        public List<TimeReg> GetTimeByAssignmentId(int id)
        {
            if (GetTimes() != null)
            {
                var newList = GetTimes().FindAll(time => time.AssignmentId.Equals(id));
                return newList;
            }

            return null;
        }
        //Finder et TimeReg-objekt med et givent Id 
        public TimeReg GetTimeById(int id)
        {
            return GetTimes().Find(time => time.TimeId.Equals(id));
        }
        //Opdaterer et TimeReg-objekt i database.
        public async Task UpdateTimeAsync(TimeReg time)
        {
            if (time != null)
            {
                await DbService.UpdateObjectAsync(time);
            }
        }
        //Returnerer alle TimeReg-objekter for et givent år.
        public List<TimeReg> GetTimeByYear(int year)
        {
            if (_times != null)
            {
                var newList = GetTimes().FindAll(time => time.Year.Equals(year));
                return newList;
            }

            return null;
        }
        //Finder TimeReg-objekter med det samme år og AssignmentId
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
        //
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
        //Bruges til at highlighte den nuværende måned på diverse sider. Blandt andet på Assignment/Details.
        public bool IsCurrentMonth(int input)
        {
            if (Models.TimeReg.CurrentMonth() == (TimeReg.MonthName) input) return true;
            return false;
        }

        //Bruges til at highlighte den nuværende måned på diverse sider. Blandt andet på Assignment/TimePlanning.
        public bool IsCurrentYear(int input)
        {
            if (DateTime.Now.Year == input) return true;
            return false;
        }
        //Opdaterer TimeRegs for Assignment i database.
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
        //Bruges til at finde summen af alle TimeReg-objekter med et givent AssignmentId
        public int GetHoursByAssignmentId(int id)
        {
            return _times.FindAll(times => times.AssignmentId == id).Sum(times => times.Hours);
        }
        //Bruges til at finde summen af en planlagte timer til en opgave ud fra måned,  år og AssignmmentId
        public int GetHoursByYearAndMonthAndAssignmentId(int year, int month, int id)
        {
            return GetTimeByYearAndMonthAndAssignmentId(year, month, id).Hours;
        }
        //Bruges til at finde summen af en planlagte timer ud fra år, måned, og en samling opgave
        public int GetHoursByYearAndMonthAndList(int year, int month, List<Assignment> assignmentList)
        {
            int counter = 0;
            foreach (var assignment in assignmentList)
            {
                counter += GetTimeByYearAndMonthAndAssignmentId(year, month, assignment.AssignmentId).Hours;
            }

            return counter;
        }

        //Finder antallet af Opgaver, der har timer registreret, som ikke er 0.
        public int AmountOfAssignmentsWithHoursInList(List<Assignment> assignments, int year, int month)
        {
            int counter = 0;
            foreach (var assignment in assignments)
            {
                counter += _times.Count(reg => reg.AssignmentId == assignment.AssignmentId && reg.Hours > 0 && reg.Year == year && reg.Month == (Models.TimeReg.MonthName)month);
            }
            return counter;
        }
    }
}
