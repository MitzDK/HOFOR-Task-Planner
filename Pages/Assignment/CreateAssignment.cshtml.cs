using System;using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class CreateAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        private List<Models.Assignment> _assignments;

        [BindProperty] public Models.Assignment Assignment { get; set; }

        public CreateAssignmentModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            _assignments = _assignmentService.GetAssignments().ToList();
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
            //Assignment.Aktion = GetUserByName(HttpContext.User.Identity.Name);
            await _assignmentService.AddAssignmentAsync(Assignment);
            return RedirectToPage("../Index");
        }
    }
}
