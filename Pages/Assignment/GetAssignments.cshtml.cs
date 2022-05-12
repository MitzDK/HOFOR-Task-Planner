using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        private static bool _isFiltered;
        private static Models.Assignment.AssignmentType _searchedAssignmentType;

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
        public GetAssignmentsModel(AssignmentService assignmentService, UserService userService, ContactService contactService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _contactService = contactService;
        }

        public IActionResult OnPost()
        {
            if (_assignmentService.FilterAssignmentType(AssignmentType).Count() < CurrentPage * PageSize)
            {
                CurrentPage = 1;
            }
            AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentType(AssignmentType),CurrentPage, PageSize);
            Count = _assignmentService.FilterAssignmentType(AssignmentType).Count();
            _isFiltered = true;
            _searchedAssignmentType = AssignmentType;
            CurrentPage = 1;
            return Page();
        }

        //public void OnGet()
        //{
        //    AssignmentList = _assignmentService.GetAssignments();
        //}
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet()
        {
            if (_isFiltered)
            {
                AssignmentType = _searchedAssignmentType;
                AssignmentList = _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentType(AssignmentType), CurrentPage, PageSize);
                Count = _assignmentService.FilterAssignmentType(AssignmentType).Count();
            }
            else
            {
                AssignmentList = _assignmentService.GetPaginatedResult(CurrentPage, PageSize);
                Count = _assignmentService.GetCounts();
            }
        }

        public IActionResult OnGetFirst()
        {
            AssignmentList = _assignmentService.FilterAssignmentType(AssignmentType);
            if (_isFiltered)
            {
                AssignmentType = _searchedAssignmentType;
                AssignmentList =
                    _assignmentService.GetPaginatedResultTest(_assignmentService.FilterAssignmentType(AssignmentType),CurrentPage, PageSize);
                Count = _assignmentService.FilterAssignmentType(AssignmentType).Count();
            }
            else
            {
                AssignmentList = _assignmentService.GetPaginatedResult(CurrentPage, PageSize);
                Count = _assignmentService.GetCounts();
            }

            return Page();
        }
        //public async Task OnGetAsync()
        //{
        //    AssignmentList = await _assignmentService.GetPaginatedResult(CurrentPage, PageSize);
        //    Count = await _assignmentService.GetCount();
        //}
    }
}
