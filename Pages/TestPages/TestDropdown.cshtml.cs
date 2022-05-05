using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.TestPages
{
    public class TestDropdownModel : PageModel
    {
        private UserService _userService;
        public List<Models.User> Users { get; set; }
        [BindProperty] public string Display { get; set; }

        public TestDropdownModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            Users = _userService.GetUsers();
        }

        public IActionResult OnPost()
        {
            var text = Display;
            return RedirectToPage("../Index");
        }
    }
}
