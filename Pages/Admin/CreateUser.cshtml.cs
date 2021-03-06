using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel
    {
        private UserService _userService;
        private PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        [BindProperty] public Models.User NewUser { get; set; }
        [BindProperty] [Display(Name = "Afdeling")] public Models.User.UserDepartments UserDepartments { get; set; }
        [BindProperty] [Display(Name = "Brugerrolle")] public Models.User.UserRoles UserRoles { get; set; }
        [BindProperty] [Display(Name = "Brugertype")] public Models.User.UserTypes UserTypes { get; set; }

        public string Message1 { get; set; } = null;
        public string Message2 { get; set; } = null;


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

            if (_userService.GetUsers().Select(us => us.UserName.ToLower()).Contains(NewUser.UserName.ToLower()) &&
                (_userService.GetUsers().Select(us => us.DisplayName.ToLower())
                    .Contains(NewUser.DisplayName.ToLower())))
            {
                Message1 = "Displayname eksisterer allerede";
                Message2 = "Username eksisterer allerede";
                return Page();
            }
            if (_userService.GetUsers().Select(us => us.DisplayName.ToLower()).Contains(NewUser.DisplayName.ToLower()))
            {
                Message1 = "Displayname eksisterer allerede";
                return Page();
            }

            if (_userService.GetUsers().Select(us => us.UserName.ToLower()).Contains(NewUser.UserName.ToLower()))
            {
                Message2 = "Username eksisterer allerede";
                return Page();
            }
            await _userService.AddUserAsync(NewUser);
            return RedirectToPage("../User/GetUsers");
        }
    }
}
