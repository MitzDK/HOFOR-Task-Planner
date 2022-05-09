using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.User
{
    public class GetUserModel : PageModel
    {
        private UserService _userService;
        private AssignmentService _assignmentService;
        public List<Models.User> Users { get; set; } = new List<Models.User>();
        public Models.User User { get; set; }
        public IEnumerable<Models.Assignment> Assignments { get; set; }

        public int Hours { get; set; }

        public GetUserModel(UserService userService, AssignmentService assignmentService)
        {
            _userService = userService;
            _assignmentService = assignmentService;
        }

        
        public IActionResult OnGet(int id)
        {
            List<Models.User> users = _userService.GetUsers();
            foreach (var VARIABLE in users)
            {
                if (VARIABLE.UserId == id)
                {
                    User = VARIABLE;
                    Assignments = _assignmentService.GetAssignments().FindAll(Us => Us.AktionUserId == User.UserId)
                        .Select(Us => Us).OrderBy(Ass => Ass.Status);
                    Hours = Assignments.Sum(It => It.Estimate);
                    return Page();
                }
            }
            return Page();
        }
    }
}
