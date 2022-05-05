using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class GetAssignmentsModel : PageModel
    {
        public List<Models.Assignment> AssignmentList;
        private AssignmentService _assignmentService;
        private UserService _userService;

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
        public GetAssignmentsModel(AssignmentService assignmentService, UserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }
        public void OnGet()
        {
            AssignmentList = _assignmentService.GetAssignments();
        }
    }
}
