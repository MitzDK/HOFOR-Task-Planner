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
    public class CreateAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;

        [BindProperty] public Models.Assignment Assignment { get; set; }
        private UserService _userService;
        public List<Models.User> Users { get; set; }
        [Display(Name = "Aktion til opgaven")]
        [BindProperty] public string AktionSearch { get; set; }
        [Display(Name = "Styring til opgaven")]
        [BindProperty] public string ControllerSearch { get; set; }

        public CreateAssignmentModel(AssignmentService assignmentService, UserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }



        public void OnGet()
        {
            Users = _userService.GetUsers();
        }

        public async Task<IActionResult> OnPost()
        {
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

            await _assignmentService.AddAssignmentAsync(Assignment);
            return RedirectToPage("../Index");
        }
    }
}