using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HOFORTaskPlanner.Pages.User
{
    public class GetUsersModel : PageModel
    {
        public IEnumerable<Models.User> UserList;
        private UserService _userService;
        [BindProperty(SupportsGet = true)] public Models.User.UserDepartments UserDepartments { get; set; }
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public Models.User PageUser { get; set; }
        [BindProperty] public string UserSearch { get; set; }

        //private static bool _isFiltered;
        //private static Models.User.UserDepartments _searchedDepartment;

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
            if (_userService.FilterTeams(UserDepartments).Count() < CurrentPage * PageSize)
            {
                CurrentPage = 1;
            }
            UserList = _userService.GetPaginatedResultList(_userService.FilterTeams(UserDepartments),CurrentPage,PageSize);
            Count = _userService.FilterTeams(UserDepartments).Count();
            Response.Cookies.Append("FilterCookie", "true");
            Response.Cookies.Append("SearchDeparment", UserDepartments.ToString());
            CurrentPage = 1;
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(UserSearch))
            {
                Response.Cookies.Delete("SearchUsername");
                UserList = _userService.GetPaginatedResult(CurrentPage, PageSize);
            }
            else
            {
                UserList = _userService.GetPaginatedResultList(_userService.GetUsersBySearch(UserSearch), CurrentPage,
                    PageSize);
                Count = _userService.GetUsersBySearch(UserSearch).Count();
                Response.Cookies.Append("SearchUsername", UserSearch);

            }
            CurrentPage = 1;
            return Page();
        }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet()
        {
            var cookieFilterValue = Request.Cookies["FilterCookie"];
            var cookieDepartmentValue = Request.Cookies["SearchDeparment"];
            var cookieUserNameValue = Request.Cookies["SearchUsername"];
            if (cookieFilterValue == "true")
            {
                Models.User.UserDepartments test;
                Enum.TryParse(cookieDepartmentValue, out test);
                UserDepartments = test;
                if (cookieUserNameValue == "true")
                {
                    UserList = _userService.GetPaginatedResultList(_userService.GetUsersByUserName(UserSearch), CurrentPage,
                        PageSize);
                    Count = _userService.GetUsersByUserName(UserSearch).Count();
                    Response.Cookies.Delete("SearchUsername");
                }
                else 
                UserList = _userService.GetPaginatedResultList(_userService.FilterTeams(UserDepartments),CurrentPage,PageSize);
                Count = _userService.FilterTeams(UserDepartments).Count();
            }
            else
            {
                UserList = _userService.GetPaginatedResultNoArchived(CurrentPage, PageSize);
                Count = _userService.GetCount();
            }
        }


        //public IActionResult OnGetFirst()
        //{
        //    UserDepartments = _userService.GetUserByUsername(HttpContext.User.Identity.Name).UserDepartment;
        //    UserList = _userService.FilterTeams(UserDepartments);
        //    Count = _userService.FilterTeams(UserDepartments).Count();
        //    return Page();
        //}
    }
}