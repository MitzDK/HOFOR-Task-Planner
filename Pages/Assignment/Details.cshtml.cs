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


        public DetailsModel(TimeService timeService, AssignmentService assignmentService, UserService userService)
        {
            _timeService = timeService;
            _assignmentService = assignmentService;
            _userService = userService;
        }

        public void OnGet(int id)
        {
            Assignments = _assignmentService.
        }
    }
}
