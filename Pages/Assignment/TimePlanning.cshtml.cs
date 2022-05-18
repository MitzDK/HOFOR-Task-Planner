using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class TimePlanningModel : PageModel
    {
        public TimeService TimeService { get; set; }
        private AssignmentService _assignmentService;
        private UserService _userService;
        public Models.Assignment Assignment { get; set; }
        public List<Models.TimeReg> Times { get; set; }
        [BindProperty]
        public List<int> TimeHours { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;

        public TimePlanningModel(TimeService timeService, AssignmentService assignmentService, UserService userService)
        {
            TimeService = timeService;
            _assignmentService = assignmentService;
            _userService = userService;
        }
        public void OnGet(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            Times = TimeService.GetTimes();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            await FinishUpdatingAsync();
            return RedirectToPage("GetAssignments");
        }
        //Returnerer et tal for hvor mange timer der i alt er planlagt for en opgave, bruges hovedsageligt til at udregne "rest"
        public int TotalTimer(int id)
        {
            return TimeService.TotalTimeForAssignmentPlanned(id);
        }
        //Bruges som en "failsafe", burde aldrig returnere null, men bruges til at få fat i en allerede eksisterende liste af "Time"
        //For en gældende opgave og år
        public List<TimeReg> ShowList(int year, int id)
        {
            List<TimeReg> newList = new List<TimeReg>();
            if (TimeService.GetTimeByYearAndAssignmentId(year, id) != null)
            {
                newList = TimeService.GetTimeByYearAndAssignmentId(year, id);
                return newList;
            }
            return null;
        }
        //Et kald ind i timeservicen, som tjekker om det input stemmer overens med Datetime.Now.Month
        public bool IsCurrentMonth(int input)
        {
            return TimeService.IsCurrentMonth(input);
        }
        //En simpel metode, som returnerer en string, der bruges til at sætte "class" inde i html koden - Her bruges den til den røde linje i bunden af tabbellen
        public string GetClassForCurrentMonth(bool isLast)
        {
            if (isLast) return "IsCurrentMonthBottom";
            return "IsCurrentMonthMiddle";
        }
        //Endnu en lidt eksperimentel metode.. Først tjekkes der om nogle "Time" allerede eksisterer i databasen til den gældende opgave, dernæst fyldes der en masse 0'er på 
        //de steder hvor der ikke allerede findes en tid
        public List<int> PopulateTable(Models.Assignment assignment)
        {
            var newlist = new List<int>();
            var templist = Times.FindAll(time => (time.AssignmentId == assignment.AssignmentId))
                .OrderBy(time => time.Year)
                .ThenBy(time => time.Month).ToList();
            for (int yearCounter = assignment.StartDate.Year; yearCounter < assignment.EndDate.Year + 1; yearCounter++)
            {
                for (int monthCounter = 1; monthCounter < 13; monthCounter++)
                {
                    if(templist.Exists(time => time.Year == yearCounter && time.Month == (Models.TimeReg.MonthName)monthCounter))
                    {
                        newlist.Add(templist.Find(time => time.Year == yearCounter && time.Month == (Models.TimeReg.MonthName)monthCounter).Hours);
                    }
                    else
                    {
                        newlist.Add(0);
                    }
                }
            }
            return newlist;
        }
        //Lidt eksperimentel metode.. Den opretter en liste, og går så igennem vores TimePlanning fra punkt til prikke.
        //Den tjekker for hver "Time", om den allerede eksisterer, hvis den gør, hentes den fra databasen og smides i listen, hvis den ikke gør, bliver der oprettet en ny.
        public async Task FinishUpdatingAsync()
        {
            var newList = new List<TimeReg>();
            int indexer = 0;
            for (int yearCounter = Assignment.StartDate.Year; yearCounter < Assignment.EndDate.Year + 1; yearCounter++)
            {
                for (int monthCounter = 1; monthCounter < 13; monthCounter++)
                {
                    if (TimeService.GetTimeByAssignmentId(Assignment.AssignmentId).Exists(time =>
                        time.AssignmentId == Assignment.AssignmentId && time.Year == yearCounter &&
                        time.Month == (TimeReg.MonthName) monthCounter))
                    {
                        var newtime =
                            TimeService.GetTimeByYearAndMonthAndAssignmentId(yearCounter, monthCounter,
                                Assignment.AssignmentId);
                        newtime.Hours = TimeHours[indexer];
                        newList.Add(newtime);
                        indexer++;
                    }
                    else
                    {
                        newList.Add(new TimeReg(yearCounter, (TimeReg.MonthName)monthCounter, TimeHours[indexer], Assignment.AssignmentId));
                        indexer++;
                    }
                }
            }
            await TimeService.AddAndUpdateTimes(newList);
        }
        //Hopper ind i vores TimeService og tjekker hvor vidt den int vi smider med er lig med Datetime.Now.Year
        public bool IsCurrentYear(int input)
        {
            return TimeService.IsCurrentYear(input);
        }
        //En ikke super fin metode, som bruges til at returnere en værdi, som direkte ændrer stylen i vores TimePlanning tabel
        public string GetClassForCurrentYear(int i)
        {
            if (TimeService.IsCurrentYear(i)) return "background-color: lightgray";
            return "";
        }
        //En ikke super fin metode, som bruges til at returnere en værdi, som direkte ændrer stylen i vores TimePlanning tabel
        public string GetClassForCurrentYearFirst(int i)
        {    
            if (TimeService.IsCurrentYear(i)) return "background-color: lightgray";
            return "border-right: 2px black;";
        }
        //Bruges til at teste datoen, som vi videre bruger til at beslutte om man skal kunne indtaste en værdi i TimePlanning eller ej
        public DateTime DateChecker(int year, int month)
        {
            return new DateTime(year, month, 1);
        }
    }
}
