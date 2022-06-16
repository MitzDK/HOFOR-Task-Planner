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
    public class GetAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        private UserService _userService;
        private ContactService _contactService;
        public TimeService TimeService { get; set; }
        public Models.Assignment Assignment { get; set; }
        public string AktionName { get; set; }
        public string ControlName { get; set; }
        public string ContactName { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
        public Models.User CurrentUser { get; set; }
        public GetAssignmentModel(AssignmentService assignmentService, UserService userService, ContactService contactService, TimeService timeService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _contactService = contactService;
            TimeService = timeService;
        }
        public void OnGet(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            AktionName = _userService.UserDisplayName(Assignment.AktionUserId);
            ControlName = _userService.UserDisplayName(Assignment.ControlUserId);
            ContactName = _contactService.ContactDisplayName(Assignment.ContactId);
            CurrentUser = AssignmentUser(Assignment.AktionUserId);
        }


        public List<TimeReg> ShowList(int id)
        {
            List<TimeReg> newList = new List<TimeReg>();
            if (TimeService.GetTimeByYearAndAssignmentId(Year, id) != null)
            {
                newList = TimeService.GetTimeByYearAndAssignmentId(Year, id);
                return newList;
            }
            return null;
        }

        public int TimeByMonth(int month, int id)
        {
            if (ShowList(id) != null)
            {
                TimeReg tempReg = ShowList(id).Find(time => time.Month == (TimeReg.MonthName)month);
                if (tempReg != null)
                {
                    return tempReg.Hours;
                }
            }
            return 0;
        }
        public Models.User AssignmentUser(int userId)
        {
            return _userService.GetUserById(userId);
        }
    }
}
