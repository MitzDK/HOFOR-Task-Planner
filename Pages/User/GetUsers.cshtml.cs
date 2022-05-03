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
        public List<Models.User> UserList;
        private UserService _userService;

        public GetUsersModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            UserList = _userService.GetUsers();
        }
    }
}
