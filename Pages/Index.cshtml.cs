using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Pages.Login;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace HOFORTaskPlanner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public void OnGet()
        {
            if (LoginPageModel.LoggedInUser == null)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        

        private UserService _userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPost()
        {
            List<Models.User> users = _userService.GetUsers();
            foreach (Models.User user in users)
            {
                if (user.UserName.ToLower().Equals(Username.ToLower()))
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        LoginPageModel.LoggedInUser = user;
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, Username)
                        };
                        if (user.UserType == Models.User.UserTypes.Admin) claims.Add(new Claim(ClaimTypes.Role, "admin"));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Admin/CreateUser");
                    }
                }
            }
            Message = "Invalid attempt";
            return Page();
        }
    }
}
