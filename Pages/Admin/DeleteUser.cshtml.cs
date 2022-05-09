using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DeleteUserModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet(int id)
        {
            User = _userService.GetUserById(id);
        }
    }
}
