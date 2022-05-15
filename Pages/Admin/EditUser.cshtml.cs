using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private PasswordHasher<string> _hasher = new PasswordHasher<string>();
        private UserService _userService;
        [BindProperty] public Models.User.UserDepartments UserDepartments { get; set; }
        [BindProperty] public Models.User.UserRoles UserRoles { get; set; }
        [BindProperty] public string FirstName { get; set; }
        [BindProperty] public string LastName { get; set; }
        [BindProperty] public string DisplayName { get; set; }
        [DataType(DataType.Password)]
        [BindProperty] public string Password { get; set; }

        public string Message { get; set; }
        

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
            Password = user.Password;
            UserDepartments = user.UserDepartment;
            UserRoles = user.UserRole;

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            
            UserToBeEdited = _userService.GetUserById(id);

            if (string.IsNullOrWhiteSpace(Password))
            {
                UserToBeEdited.Password = UserToBeEdited.Password;
            }
            else
            {
                UserToBeEdited.Password = _hasher.HashPassword(null, Password);

            }
            UserToBeEdited.FirstName = FirstName;
            UserToBeEdited.LastName = LastName;

            var test = _userService.GetUsers().Where(user => user.DisplayName == DisplayName);
            if(test.Count() == 0)
            {
                UserToBeEdited.DisplayName = DisplayName;
            }
            else
            {
                Message = "Displayname eksisterer allerede";
                return Page();
            }
            
            UserToBeEdited.DisplayName = DisplayName;
            UserToBeEdited.UserRole = UserRoles;
            UserToBeEdited.UserDepartment = UserDepartments;


            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.UpdateUserAsync(UserToBeEdited);
            return RedirectToPage("../User/GetUsers");
        }

        public async Task<IActionResult> OnPostArchive(int id)
        {
            UserToBeEdited = _userService.GetUserById(id);
            UserToBeEdited.Password = "Arkiveretbruger";
            UserToBeEdited.UserType = Models.User.UserTypes.Arkiveret;
            await _userService.UpdateUserAsync(UserToBeEdited);
            return RedirectToPage("../User/GetUsers");
        }
    }
}
