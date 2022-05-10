using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private UserService _userService;
        [BindProperty] public Models.User.UserDepartments UserDepartments { get; set; }
        [BindProperty] public Models.User.UserRoles UserRoles { get; set; }
        [BindProperty] public string FirstName { get; set; }
        [BindProperty] public string LastName { get; set; }
        [BindProperty] public string DisplayName { get; set; }
        public Models.User UserToBeEdited { get; set; }


        public EditUserModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            UserToBeEdited = _userService.GetUserById(id);
            var user = _userService.GetUserById(id);
            FirstName = user.FirstName;
            LastName = user.LastName;
            DisplayName = user.DisplayName;
            UserDepartments = user.UserDepartment;
            UserRoles = user.UserRole;

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var user = _userService.GetUserById(id);
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.DisplayName = DisplayName;
            user.UserRole = UserRoles;
            user.UserDepartment = UserDepartments;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.UpdateUserAsync(user);
            return RedirectToPage("../User/GetUsers");
        }
    }
}
