using System;using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class CreateAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        private UserService _userService;

        [BindProperty] public Models.Assignment Assignment { get; set; }

        public CreateAssignmentModel(AssignmentService assignmentService, UserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Assignment.AktionUserId = LoginPageModel.LoggedInUser.UserId;
            Assignment.Aktion = _userService.GetUserById(Assignment.AktionUserId);
            Assignment.ControlUserId = LoginPageModel.LoggedInUser.UserId;
            Assignment.Control = _userService.GetUserById(Assignment.ControlUserId);
            await _assignmentService.AddAssignmentAsync(Assignment);
            return RedirectToPage("../Index");
        }
    }
}
