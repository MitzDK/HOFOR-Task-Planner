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
        private TimeService _timeService;
        private AssignmentService _assignmentService;
        private UserService _userService;
        public List<Models.Assignment> Assignments { get; set; }
        public List<Models.Time> Times { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;


        public DetailsModel(TimeService timeService, AssignmentService assignmentService, UserService userService)
        {
            _timeService = timeService;
            _assignmentService = assignmentService;
            _userService = userService;
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

        public List<Time> ShowList(int year, int id)
        {
            List<Time> newList = new List<Time>();
            if (_timeService.GetTimeByYearAndAssignmentId(year, id) != null)
            {
                newList = _timeService.GetTimeByYearAndAssignmentId(year, id);
                return newList;
            }
            return null;
        }

        public int TimeByMonth(int month, int year, int id)
        {
            if (ShowList(year, id) != null)
            {
                var list = ShowList(year, id);
                if (ShowList(year, id).Find(time => time.Month == (Time.MonthName)month) != null)
                {
                    return ShowList(year, id).Find(time => time.Month == (Time.MonthName)month).Hours;
                }
            }
            return 0;
        }
        public int TotalTimer(int id)
        {
            return _timeService.TotalTimeForAssignmentPlanned(id);
        }
        public void OnGet(int id)
        {
            Assignments = _assignmentService.GetAssignmentsByUserId(id);
        }
    }
}
