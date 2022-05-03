using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Login
{
    public class LoginPageModel : PageModel
    {
        public static User LoggedInUser { get; set; }

        //private UserService _userService;

        //public LoginPageModel(userService userService)
        //{
        //    _userService = userService;
        //}
        [BindProperty] 
        public string Username { get; set; } 

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPost()
        {
            List<User> users = _userService.Users;
            foreach (User user in users)
            {
                var passwordHasher = new PasswordHasher<string>();
                if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                {
                    LoggedInUser = user;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Username)
                    };

                    if (Username == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

                    var claimsIdentity =
                        new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("/Index");
                }
            }

            Message = "Invalid attempt";
            return Page();
        }





        public void OnGet()
        {
        }
    }
}
