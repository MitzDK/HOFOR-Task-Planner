using System;
using System.Collections.Generic;
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
        public Models.Assignment AssignmentToBeUpdated { get; set; }
        public int AktionUserId { get; set; }
        public int ControlUserId { get; set; }

        public EditAssignmentModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        public IActionResult OnGet(int id)
        {
            Assignment =  _assignmentService.GetAssignmentById(id);
            Assignment.AssignmentId = id;
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            Assignment.AssignmentId = id;
            AssignmentToBeUpdated = _assignmentService.GetAssignmentByIdAsync(id).Result;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Assignment.AktionUserId = AssignmentToBeUpdated.AktionUserId;
            Assignment.ControlUserId = AssignmentToBeUpdated.ControlUserId;
            await _assignmentService.UpdateAssignmentAsync(Assignment);
            return RedirectToPage("GetAssignments");
        }
    }
}
