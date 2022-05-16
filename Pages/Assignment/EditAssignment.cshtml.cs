using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class EditAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        private UserService _userService;
        private ContactService _contactService;

        [BindProperty] public Models.Assignment Assignment { get; set; }
        public List<Models.User> Users { get; set; }
        public List<Models.Contact> Contacts { get; set; }
        [Display(Name = "Aktion til opgaven")]
        [BindProperty] public string AktionSearch { get; set; }
        [Display(Name = "Styring til opgaven")]
        [BindProperty] public string ControllerSearch { get; set; }
        [Display(Name = "Kontakt til opgaven")]
        [BindProperty] public string ContactSearch { get; set; }
        
        public EditAssignmentModel(AssignmentService assignmentService, UserService userService, ContactService contactService)
        {
            _assignmentService = assignmentService;
            _contactService = contactService;
            _userService = userService;
            _contactService = contactService;
        }
        public IActionResult OnGet(int id)
        {
            Assignment = _assignmentService.GetAssignmentById(id);
            Users = _userService.GetUsers();
            Contacts = _contactService.GetContacts();
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            Assignment.AssignmentId = id;
            Users = _userService.GetUsers();
            Contacts = _contactService.GetContacts();


            if (string.IsNullOrWhiteSpace(Assignment.Comment)) Assignment.Comment = " ";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userService.GetUserByDisplayName(AktionSearch) != null)
            {
                Assignment.AktionUserId = _userService.GetUserByDisplayName(AktionSearch).UserId;
            }
            else
            {
                Assignment.AktionUserId = 0;
            }
            if (_userService.GetUserByDisplayName(ControllerSearch) != null)
            {
                Assignment.ControlUserId = _userService.GetUserByDisplayName(ControllerSearch).UserId;
            }
            else
            {
                Assignment.ControlUserId = 0;
            }
            if (_contactService.GetContactByEmail(ContactSearch) != null)
            {
                Assignment.ContactId = _contactService.GetContactByEmail(ContactSearch).ContactId;
            }
            else
            {
                Assignment.ContactId = 0;
            }
            await _assignmentService.UpdateAssignmentAsync(Assignment);
            return RedirectToPage("GetAssignments");
        }
        public string UserDisplayName(int userId)
        {
            if (_userService.GetUserById(userId) != null)
            {
                return _userService.GetUserById(userId).DisplayName;
            }
            return "N/A";
        }
        
        
        
    }
}
