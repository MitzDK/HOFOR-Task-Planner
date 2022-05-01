using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.TestPages
{
    public class CreateAktionModel : PageModel
    {
        private AktionService _aktionService;
        [BindProperty] public Aktion NewAktion { get; set; }
        [BindProperty] public Aktion.UserDepartments UserDepartments { get; set; }
        [BindProperty] public Aktion.UserRoles UserRoles { get; set; }
        [BindProperty] public Aktion.UserTypes UserTypes { get; set; }

        public CreateAktionModel(AktionService aktionService)
        {
            _aktionService = aktionService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            NewAktion.UserDepartment = UserDepartments;
            NewAktion.UserRole = UserRoles;
            NewAktion.UserType = UserTypes;
            NewAktion.LastUpdated = DateTime.Now;

            //TEMPOARY SOLUTION TILL WE HOOKUP A DATABASE
            //Once we use a database entityframework will automatically generate a userId, till then
            //We do it like this :)
            //NewAktion.UserId = _aktionService.GetAktions().Count + 1;

            await _aktionService.AddAktionAsync(NewAktion);
            return RedirectToPage("GetAktions");
        }
    }
}
