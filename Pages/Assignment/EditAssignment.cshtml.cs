using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class EditAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        [BindProperty] public Models.Assignment Assignment { get; set; }
        private UserService _userService;
        public List<Models.User> Users { get; set; }
        public Models.Assignment AssignmentToBeUpdated { get; set; }
        [Display(Name = "Aktion til opgaven")]
        [BindProperty] public string AktionSearch { get; set; }
        [Display(Name = "Styring til opgaven")]
        [BindProperty] public string ControllerSearch { get; set; }

        public EditAssignmentModel(AssignmentService assignmentService, UserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            Users = _userService.GetUsers();
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            Assignment.AssignmentId = id;
            Users = _userService.GetUsers();
            //AssignmentToBeUpdated = _assignmentService.GetAssignmentByIdAsync(id).Result;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userService.GetUserByDisplayName(AktionSearch) != null)
            {
                Assignment.AktionUserId = _userService.GetUserByDisplayName(AktionSearch).UserId;
            }
            else
            {
                Assignment.AktionUserId = 0;
            }
            if (_userService.GetUserByDisplayName(ControllerSearch) != null)
            {
                Assignment.ControlUserId = _userService.GetUserByDisplayName(ControllerSearch).UserId;
            }
            else
            {
                Assignment.ControlUserId = 0;
            }

            //Assignment.AktionUserId = AssignmentToBeUpdated.AktionUserId;
            //Assignment.ControlUserId = AssignmentToBeUpdated.ControlUserId;
            await _assignmentService.UpdateAssignmentAsync(Assignment);
            return RedirectToPage("GetAssignments");
        }
    }
}
