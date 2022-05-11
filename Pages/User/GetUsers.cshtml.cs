using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
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
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public Models.User User { get; set; }

        public GetUsersModel(UserService userService)
        {
            _userService = userService;
        }
        //public void OnGet()
        //{
        //    UserList = _userService.GetUsers();
        //}

        public IActionResult OnPost()
        {
            UserList = _userService.FilterTeams(UserDepartments);
            return Page();
        }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet()
        {
            UserList = _userService.GetPaginatedResult(CurrentPage, PageSize);
            Count = _userService.GetCount();
        }

        public IActionResult OnGetFirst()
        {
            UserList = _userService.FilterTeams(LoginPageModel.LoggedInUser.UserDepartment);
            UserDepartments = LoginPageModel.LoggedInUser.UserDepartment;
            return Page();
        }
    }
}