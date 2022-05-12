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
        [BindProperty]
        public Models.Assignment Assignment { get; set; }
        public List<Models.User> Users { get; set; }
        public string AktionName { get; set; }
        public string ControlName { get; set; }
        public string ContactName { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;

        public GetAssignmentModel(AssignmentService assignmentService, UserService userService, ContactService contactService, TimeService timeService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _contactService = contactService;
            TimeService = timeService;
        }

        public string UserDisplayName(int userId)
        {
            if (_userService.GetUserById(userId) != null)
            {
                return _userService.GetUserById(userId).DisplayName;
            }
            return "N/A";
        }

        public string ContactDisplayName(int contactId)
        {
            if (_contactService.GetContactById(contactId) != null)
            {
                return _contactService.GetContactById(contactId).FirstName + " " + _contactService.GetContactById(contactId).LastName;
            }
            return "N/A";
        }

        public void OnGet(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            Users = _userService.GetUsers();
            AktionName = UserDisplayName(Assignment.AktionUserId);
            ControlName = UserDisplayName(Assignment.ControlUserId);
            ContactName = ContactDisplayName(Assignment.ContactId);
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
