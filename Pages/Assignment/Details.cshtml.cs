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
    public class DetailsModel : PageModel
    {
        public TimeService TimeService { get; set; }
        private AssignmentService _assignmentService;
        private UserService _userService;
        public List<Models.Assignment> Assignments { get; set; }
        public List<Models.TimeReg> Times { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;


        public DetailsModel(TimeService timeService, AssignmentService assignmentService, UserService userService)
        {
            TimeService = timeService;
            _assignmentService = assignmentService;
            _userService = userService;
        }

        public void OnGet(int id)
        {
            Assignments = _assignmentService.GetAssignmentsByUserId(id);
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

        public Models.User AssignmentUser(int userId)
        {
            return _userService.GetUserById(userId);
        }
        public string UserDisplayName(int userId)
        {
            if (_userService.GetUserById(userId) != null)
            {
                return _userService.GetUserById(userId).DisplayName;
            }
            return "N/A";
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

        public int TimeByMonth(int month, int year, int id)
        {
            if (ShowList(year, id) != null)
            {
                var list = ShowList(year, id);
                if (ShowList(year, id).Find(time => time.Month == (TimeReg.MonthName)month) != null) return ShowList(year, id).Find(time => time.Month == (TimeReg.MonthName)month).Hours;
            }
            return 0;
        }

        
    }
}
