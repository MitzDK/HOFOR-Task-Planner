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

        public GetAssignmentsModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        public void OnGet()
        {
            AssignmentList = _assignmentService.GetAssignments();
        }
    }
}
