using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Admin
{
    public class CreateUserModel : PageModel
    {
        private UserService _userService;
        private PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        [BindProperty] public Models.User NewUser { get; set; }


        [BindProperty] public Models.User.UserDepartments UserDepartments { get; set; }
        [BindProperty] public Models.User.UserRoles UserRoles { get; set; }
        [BindProperty] public Models.User.UserTypes UserTypes { get; set; }

        public string Message { get; set; } = null;

        public CreateUserModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewUser.UserDepartment = UserDepartments;
            NewUser.UserRole = UserRoles;
            NewUser.UserType = UserTypes;
            NewUser.LastUpdated = DateTime.Now;
            NewUser.Password = _passwordHasher.HashPassword(null, NewUser.Password);
            if (_userService.GetUsers().Select(us => us.DisplayName.ToLower()).Contains(NewUser.DisplayName.ToLower()))
            {
                Message = "Displayname eksisterer allerede";
                return Page();
            }
            await _userService.AddUserAsync(NewUser);
            return RedirectToPage("../User/GetUsers");
        }
    }
}
