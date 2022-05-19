using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    [Authorize(Roles = "admin")]
    public class DashboardModel : PageModel
    {
        private UserService _userService;
        private TimeService _timeService;
        private AssignmentService _assignmentService;

        public List<Models.User> Users { get; set; }
        public List<Models.TimeReg> TimeRegs { get; set; }
        public List<Models.Assignment> Assignments { get; set; }
        public string UserDepartment { get; set; } = "N/A";
        [BindProperty(SupportsGet = true)] public Models.User.UserDepartments UserDepartments { get; set; }

        // Pagination
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 6;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public DashboardModel(UserService userService, TimeService timeService, AssignmentService assignmentService)
        {
            _userService = userService;
            _timeService = timeService;
            _assignmentService = assignmentService;
        }
        public void OnGet()
        {
            var cookieDepartmentValue = Request.Cookies["DashboardSearchDeparment"];
            UserDepartments = (Models.User.UserDepartments)Convert.ToInt32(cookieDepartmentValue);
            UserDepartment = ((Models.User.UserDepartments)Convert.ToInt32(cookieDepartmentValue)).ToString();
            Users = _userService.GetUsersByDepartment(UserDepartments);
            Users = _userService.GetPaginatedNoLeaderRole(Users, CurrentPage, PageSize);
            Count = _userService.GetUsersByDepartment(UserDepartments)
                .Where(us => us.UserRole != Models.User.UserRoles.Leder && us.UserType != Models.User.UserTypes.Arkiveret).ToList().Count;
        }
        public IActionResult OnPost()
        {
            CurrentPage = 1;
            UserDepartment = UserDepartments.ToString();
            if (UserDepartment == "0")
            {
                Models.User user = _userService.GetUserByUsername(HttpContext.User.Identity.Name);
                UserDepartment = user.UserDepartment.ToString();
                UserDepartments = user.UserDepartment;
            }
            Users = _userService.GetPaginatedNoLeaderRole(_userService.FilterTeams(UserDepartments).ToList(), CurrentPage, PageSize);
            Count = _userService.GetUsersByDepartment(UserDepartments)
                .Where(us => us.UserRole != Models.User.UserRoles.Leder && us.UserType != Models.User.UserTypes.Arkiveret).ToList().Count;
            Response.Cookies.Append("DashboardSearchDeparment", ((int)UserDepartments).ToString());
            return Page();
        }
        
        //Returnerer værdien for det samlede antal af timer planlagt for den gældende bruger
        public int GetTotalHoursByUserId(int userId)
        {
            int counter = 0;
            foreach (var assignment in _assignmentService.GetAssignmentsByUserId(userId))
            {
                counter += _timeService.GetHoursByAssignmentId(assignment.AssignmentId);
            }
            return counter;
        }
        //Returnerer alle opgaver for den gældende bruger
        public List<Models.Assignment> UserAssignments(int userId)
        {
            return _assignmentService.GetAssignmentsByUserId(userId);
        }

        //Returnerer en samlet mængde af planlagte timer for en gældende bruger, pr. år og måned
        public int GetTotalHoursByYearAndMonthAndUserid(int year, int month, int userId)
        {
            int counter = 0;
            foreach (var assignment in _assignmentService.GetAssignmentsByUserId(userId))
            {
                counter += _timeService.GetHoursByYearAndMonthAndAssignmentId(year, month, assignment.AssignmentId);
            }
            return counter;
        }
        //Returnerer hvor mange timer der er for den gældende department, ud fra årstallet og måned
        public int GetHoursByYearAndMonthAndList(int year, int month, Models.User.UserDepartments userDepartment)
        {
            return _timeService.GetHoursByYearAndMonthAndList(year, month,
                _assignmentService.AssignmentsForYearAndMonthAndUserDepartment(year, month, userDepartment));
        }
        //Returnerer den totale mængde af opgaver, for det gældende år og måned, for det specifikke brugerområde
        public int AmountOfAssignmentsForYearAndMonthAndUserDepartment(int year, int month,
            Models.User.UserDepartments department)
        {
            return _assignmentService.AmountOfAssignmentsForYearAndMonthAndUserDepartment(year, month, department);
        }
        //Returnerer den totale mængde af opgaver, for det gældende år og måned, for det specifikke brugerid
        public int AmountOfAssignmentsForYearAndMonthAndUserId(int year, int month, int userId)
        {
            return _assignmentService.AmountOfAssignmentsForYearAndMonthAndUserId(year, month, userId);
        }
        //En simpel metode som returnerer en string, som bliver brugt i HTMLen til at sætte en farve på timerne, baseret på inputtet 
        //Denne metode skal senere laves om, så ledere kan ændre i tallene pr. bruger
        public string ColourByHours(int hours)
        {
            switch (hours)
            {
                case var n when (n<130):
                    return "background-color: #fff2cc";
                case var n when n is >= 130 and <= 150:
                    return "background-color: #92d050";
                case var n when n is >= 151 and <= 160:
                    return "background-color: #f8cbad";
                case var n when (n > 160):
                    return "background-color: #ed7d31";
                default:
                    return "background-color: #fff2cc";
            }
        }
        //En metode som skaffer et total, for mængden af opgaver for en bruger, som har timer i sig
        public int AmountOfAssignmentsWithHoursInList(int year, int month, int userId)
        {
           return _timeService.AmountOfAssignmentsWithHoursInList(
                _assignmentService.AssigmentsForDateAndUserId(year, month, userId), year, month);
        }


    }
}
