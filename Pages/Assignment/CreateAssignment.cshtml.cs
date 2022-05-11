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
    public class CreateAssignmentModel : PageModel
    {
        private AssignmentService _assignmentService;
        private UserService _userService;
        private ContactService _contactService;

        public List<string> DescriptionList = new List<string>();

        [BindProperty] public Models.Assignment Assignment { get; set; }
        public List<Models.User> Users { get; set; }
        public List<Models.Contact> Contacts { get; set; }
        [Display(Name = "Aktion til opgaven")]
        [BindProperty] public string AktionSearch { get; set; }
        [Display(Name = "Styring til opgaven")]
        [BindProperty] public string ControllerSearch { get; set; }
        [Display(Name = "Kontakt til opgaven")]
        [BindProperty] public string ContactSearch { get; set; }
        [Display(Name = "Beskrivelse")]
        [BindProperty] public string DescriptionSearch { get; set; }
        

        public CreateAssignmentModel(AssignmentService assignmentService, UserService userService, ContactService contactService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _contactService = contactService;
        }



        public void OnGet()
        {
            DescriptionList = new List<string>();
            foreach (var description in _assignmentService.GetAssignments().Select(De=>De.Description))
            {
                if (!DescriptionList.Contains(description))
                {
                    DescriptionList.Add(description);
                }
            }
            Contacts = _contactService.GetContacts();
            Users = _userService.GetUsers();
        }

        public async Task<IActionResult> OnPost()
        {
            Contacts = _contactService.GetContacts();
            Users = _userService.GetUsers();
            Assignment.Description = DescriptionSearch;
            if (string.IsNullOrWhiteSpace(Assignment.Comment)) Assignment.Comment = " ";
            if (string.IsNullOrWhiteSpace(Assignment.Description)) Assignment.Description = " ";
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

            if (!ModelState.IsValid)
            {
                var test = ModelState;
                return Page();
            }
            //_assignmentService.AddAssignment(Assignment);
            await _assignmentService.AddAssignmentAsync(Assignment);
            return RedirectToPage("../User/GetUsers");
        }
    }
}