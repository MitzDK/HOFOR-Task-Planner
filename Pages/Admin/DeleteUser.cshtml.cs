using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.MockData;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Admin
{
    // [Authorize(Roles = "admin")]
    public class DeleteUserModel : PageModel
    {
        private UserService _userService;
        public Models.User User { get; set; }
        public List<Models.User> Users { get; set; }

        public DeleteUserModel(UserService userService)
        {
            _userService = userService;
            Users = _userService.GetUsers();
        }

        public void OnGet(int id)
        {
            User = _userService.GetUserById(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User.Password = "Arkiveretbruger";
            User.UserType = Models.User.UserTypes.Arkiveret;
            await _userService.UpdateUserAsync(User);
            return RedirectToPage("../User/GetUsers");
        }
    }
}
