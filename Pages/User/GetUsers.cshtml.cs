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
            CurrentPage = 1;
            UserList = _userService.GetPaginatedResultList(_userService.FilterTeams(UserDepartments),CurrentPage,PageSize);
            Count = _userService.FilterTeams(UserDepartments).Count();
            Response.Cookies.Append("UserSearchDepartment", ((int)UserDepartments).ToString());

            return Page();
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(UserSearch))
            {
                //TODO Skal fikses med anden metode
                //Response.Cookies.Delete("SearchUsername");
                UserList = _userService.GetPaginatedResult(CurrentPage, PageSize);
            }
            else
            {
                UserList = _userService.GetPaginatedResultList(_userService.GetUsersBySearch(UserSearch), CurrentPage,
                    PageSize);
                Count = _userService.GetUsersBySearch(UserSearch).Count();
                Response.Cookies.Append("UserSearchUsername", UserSearch);
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
            var cookieDepartmentValue = Request.Cookies["UserSearchDepartment"];
            //var cookieUserNameValue = Request.Cookies["SearchUsername"];
            //Martin m� lige forklare
            //if (cookieUserNameValue == "true")
            //{
            //    UserList = _userService.GetPaginatedResultTest(_userService.GetUsersByUserName(UserSearch),
            //        CurrentPage,
            //        PageSize);
            //    Count = _userService.GetUsersByUserName(UserSearch).Count();
            //    //Fix delete cookie
            //    //Response.Cookies.Delete("SearchUsername");
            //}
            //else
            //{
            //    UserList = _userService.GetPaginatedResultTest(_userService.FilterTeams(UserDepartments), 
            //        CurrentPage, PageSize);
            //    Count = _userService.FilterTeams(UserDepartments).Count();
            //}
            UserDepartments = (Models.User.UserDepartments)Convert.ToInt32(cookieDepartmentValue);
            UserList = _userService.GetPaginatedResultList(_userService.FilterTeams(UserDepartments), CurrentPage, PageSize);
            Count = _userService.FilterTeams(UserDepartments).Count();
        }
    }
}