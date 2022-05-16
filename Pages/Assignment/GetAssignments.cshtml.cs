using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class GetAssignmentsModel : PageModel
    {
        public IEnumerable<Models.Assignment> AssignmentList;
        private AssignmentService _assignmentService;
        private UserService _userService;
        private ContactService _contactService;
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        [BindProperty(SupportsGet = true)] public Models.Assignment.AssignmentType AssignmentType { get; set; }
        [BindProperty(SupportsGet = true)] public string Description { get; set; }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;


        public List<string> DescriptionList { get; set; }

        public GetAssignmentsModel(AssignmentService assignmentService, UserService userService, ContactService contactService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _contactService = contactService;
        }
        public void OnGet()
        {
            DescriptionList = new List<string>();
            foreach (var description in _assignmentService.GetAssignments().Select(De => De.Description))
            {
                if (!DescriptionList.Contains(description))
                {
                    DescriptionList.Add(description);
                }
            }
            var cookieIsFilteredValue = Request.Cookies["AssignmentFilterCookie"];
            var cookieIsFilteredDescription = Request.Cookies["AssignmentDescriptionCookie"];
            var cookiesFilterTypeValue = Request.Cookies["AssignmentTypeSelect"];
            var cookiesFilterDescription = Request.Cookies["AssignmentFilterDescription"];
            if (cookieIsFilteredValue == "true")
            {
                AssignmentType = (Models.Assignment.AssignmentType)Convert.ToInt32(cookiesFilterTypeValue);
                AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentType(AssignmentType), CurrentPage, PageSize);
                Count = _assignmentService.FilterAssignmentType(AssignmentType).Count();
            }
            else if (cookieIsFilteredDescription == "true")
            {
                Description = cookiesFilterDescription;
                AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentDescription(Description), CurrentPage, PageSize);
                Count = _assignmentService.FilterAssignmentDescription(Description).Count();
            }
            else
            {
                AssignmentList = _assignmentService.GetPaginatedResult(CurrentPage, PageSize);
                Count = _assignmentService.GetCounts();
            }
        }
        public IActionResult OnPost()
        {
            DescriptionList = new List<string>();
            foreach (var description in _assignmentService.GetAssignments().Select(De => De.Description))
            {
                if (!DescriptionList.Contains(description))
                {
                    DescriptionList.Add(description);
                }
            }
            CurrentPage = 1;
            AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentType(AssignmentType),CurrentPage, PageSize);
            Count = _assignmentService.FilterAssignmentType(AssignmentType).Count();
            Response.Cookies.Append("AssignmentFilterCookie", "true");
            Response.Cookies.Append("AssignmentTypeSelect", ((int)AssignmentType).ToString());
            CurrentPage = 1;
            return Page();
        }

        public IActionResult OnPostDescriptionFilter()
        {
            DescriptionList = new List<string>();
            foreach (var description in _assignmentService.GetAssignments().Select(De => De.Description))
            {
                if (!DescriptionList.Contains(description))
                {
                    DescriptionList.Add(description);
                }
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                AssignmentList = _assignmentService.GetPaginatedResult(CurrentPage, PageSize);
            }
            else
            {
                AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentDescription(Description),
                    CurrentPage, PageSize);
                Count = _assignmentService.FilterAssignmentDescription(Description).Count();
                Response.Cookies.Append("AssignmentDescriptionCookie", "true"); // NY
                Response.Cookies.Append("AssignmentFilterDescription", ((string)Description).ToString());
            }
            CurrentPage = 1;
            return Page();
        }
        
        public int GetHoursByAssignmentId(int id)
        {
            return _assignmentService.GetHoursByAssignmentId(id);
        }

        public void OnGetSortByContact()
        {
            Response.Cookies.Append("AssignmentSort", "contact");
            Response.Cookies.Append("AssignmentIsSorted", "true");
            OnGet();
        }

        public IActionResult OnGetSortByEstimate()
        {
            AssignmentList = _assignmentService.SortByEstimate();
            return Page();
        }

        public IActionResult OnGetSortByEstimateDescending()
        {
            AssignmentList = _assignmentService.SortByEstimateDescending();
            return Page();
        }

        public IActionResult OnGetSortByStartDate()
        {
            AssignmentList = _assignmentService.SortByStartDate();
            return Page();
        }
        public IActionResult OnGetSortByStartDateDescending()
        {
            AssignmentList = _assignmentService.SortByStartDateDescending();
            return Page();
        }

        public IActionResult OnGetSortByEndDate()
        {
            AssignmentList = _assignmentService.SortByEndDate();
            return Page();
        }
        public IActionResult OnGetSortByEndDateDescending()
        {
            AssignmentList = _assignmentService.SortByEndDateDescending();
            return Page();
        }

        public IActionResult OnGetSortByRemaining()
        {
            AssignmentList = _assignmentService.SortByRemaining();
            return Page();
        }

        public IActionResult OnGetSortByRemainingDescending()
        {
            AssignmentList = _assignmentService.SortbyRemainingDescending();
            return Page();
        }

        public Models.User AssignmentUser(int userId)
        {
            return _userService.GetUserById(userId);
        }
        public string UserDisplayName(int userId)
        {
            if (_userService.GetUserById(userId) != null)
            {
                return _userService.GetUserById(userId).DisplayName;
            }
            return "N/A";
        }

        public string ContactDisplayName(int contactId)
        {
            if (_contactService.GetContactById(contactId) != null)
            {
                return _contactService.GetContactById(contactId).FirstName + " " + _contactService.GetContactById(contactId).LastName;
            }
            return "N/A";
        }

    }
}
