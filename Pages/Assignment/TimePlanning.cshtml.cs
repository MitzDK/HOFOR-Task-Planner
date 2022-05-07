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
        }

        public int TotalTimer(int id)
        {
            return TimeService.TotalTimeForAssignmentPlanned(id);
        }
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
        public bool IsCurrentMonth(int input)
        {
            return TimeService.IsCurrentMonth(input);
        }
        public string GetClassForCurrentMonth(bool isLast)
        {
            if (isLast) return "IsCurrentMonthBottom";
            return "IsCurrentMonthMiddle";
        }

        public List<int> PopulateTable(Models.Assignment assignment)
        {
            var newlist = new List<int>();
            for (int i = 0; i < (assignment.EndDate.Year - assignment.StartDate.Year + 1); i++)
            {
                for (int newint = 0; newint < 12; newint++)
                {
                    newlist.Add(0);
                }
            }
            return newlist;
        }
        
    }
}
