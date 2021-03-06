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
    public class GetAktionsModel : PageModel
    {
        public List<Aktion> AktionList;
        private AktionService _aktionService;

        public GetAktionsModel(AktionService aktionService)
        {
            _aktionService = aktionService;
        }
        public void OnGet()
        {
            AktionList = _aktionService.GetAktions();
        }

    }
}
