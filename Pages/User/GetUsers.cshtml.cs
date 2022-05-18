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

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public GetUsersModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            var cookieDepartmentValue = Request.Cookies["UserSearchDepartment"];
            if (cookieDepartmentValue != "0")
            {
                UserDepartments = (Models.User.UserDepartments)Convert.ToInt32(cookieDepartmentValue);
                UserList = _userService.GetPaginatedResultList(_userService.FilterTeams(UserDepartments), CurrentPage, PageSize);
                Count = _userService.FilterTeams(UserDepartments).Count();
            }
            else
            {
                UserDepartments = 0;
                UserList = _userService.GetPaginatedResult(CurrentPage, PageSize);
                Count = _userService.GetCount();
            }
        }
        //OnPost bruges til filtrering efter Department (enhed). Der oprettes cookies så filtreringen er bevaret i forbindelse med Pagination eller navigering væk fra siden.

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
            Response.Cookies.Append("UserSearchDepartment", "", new CookieOptions{Expires = DateTime.Now.AddDays(-1D)});
            if (string.IsNullOrWhiteSpace(UserSearch))
            {
                //TODO Skal fikses med anden metode
                Response.Cookies.Append("UserSearchUsername", "", new CookieOptions{Expires = DateTime.Now.AddDays(-1D)});
                UserList = _userService.GetPaginatedResult(CurrentPage, PageSize);
                Count = _userService.GetCount();
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

        
    }
}