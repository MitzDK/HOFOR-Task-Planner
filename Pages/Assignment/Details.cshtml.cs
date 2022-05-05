using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Assignment
{
    public class DetailsModel : PageModel
    {
        private TimeService _timeService;
        private List<Time> _times;
        public DetailsModel(TimeService timeService)
        {
            _timeService = timeService;
        }

        public void OnGet()
        {
        }
    }
}
