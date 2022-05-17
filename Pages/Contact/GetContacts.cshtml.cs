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
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages =>(int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public GetContactsModel(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void OnGet()
        {
            Contacts = _contactService.GetPaginatedResultList(_contactService.GetContacts(), CurrentPage, PageSize);
            Count = _contactService.GetContacts().Count();
        }

        public IActionResult OnPost()
        {
            CurrentPage = 1;
            Contacts = _contactService.GetPaginatedResultList(_contactService.GetContacts(), CurrentPage, PageSize);
            Count = _contactService.GetContacts().Count();


            return Page();
        }
    }
}
