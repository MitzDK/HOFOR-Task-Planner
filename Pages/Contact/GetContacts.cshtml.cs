using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Contact
{
    public class GetContactsModel : PageModel
    {
        public List<Models.Contact> Contacts { get; set; }
        private ContactService _contactService;

        public GetContactsModel(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void OnGet()
        {
            Contacts = _contactService.GetContacts();
        }
    }
}
