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

        [BindProperty] public Models.Assignment Assignment { get; set; }

        public CreateAssignmentModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
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
            Assignment.ControlUserId = LoginPageModel.LoggedInUser.UserId;
            await _assignmentService.AddAssignmentAsync(Assignment);
            return RedirectToPage("../Index");
        }
    }
}
