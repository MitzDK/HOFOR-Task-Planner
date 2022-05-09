using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Contact
{
    public class EditContactModel : PageModel
    {
        private ContactService _contactService;
        [BindProperty]
        public Models.Contact Contact { get; set; }

        public EditContactModel(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void OnGet(int id)
        {
            Contact = _contactService.GetContactById(id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Contact.ContactId = id;

            await _contactService.UpdateContactAsync(Contact);
            return RedirectToPage("GetContacts");
        }
    }
}
