using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Contact
{
    public class CreateContactModel : PageModel
    {
        private ContactService _contactService;
        [BindProperty] public Models.Contact Contact { get; set; }

        public CreateContactModel(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _contactService.AddContact(Contact);
           return RedirectToPage("../Index");
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    await _contactService.AddContactAsync(Contact);
        //    return RedirectToPage("../Index");
        //}
    }
}
