using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class DashboardModel : PageModel
    {
        private UserService _userService;
        private TimeService _timeService;
        private AssignmentService _assignmentService;

        public List<Models.User> Users { get; set; }

        public void OnGet()
        {

        }
    }
}
