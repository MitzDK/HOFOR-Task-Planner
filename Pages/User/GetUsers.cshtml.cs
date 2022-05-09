using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.User
{
    public class GetUsersModel : PageModel
    {
        public IEnumerable<Models.User> UserList;
        private UserService _userService;
        [BindProperty] public Models.User.UserDepartments UserDepartments { get; set; }

        public Models.User User { get; set; }

        public GetUsersModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            UserList = _userService.GetUsers();
        }

        public IActionResult OnPost()
        {
            UserList = _userService.FilterTeams(UserDepartments);
            return Page();
        }
    }
}
