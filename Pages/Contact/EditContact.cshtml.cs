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
        public Models.Contact Contact { get; set; }

        public EditContactModel(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void OnGet(int id)
        {
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _contactService.UpdateContactAsync(Contact);
            return RedirectToPage("../Index");
        }
    }
}
