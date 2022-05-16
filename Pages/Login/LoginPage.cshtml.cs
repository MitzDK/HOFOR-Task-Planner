using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOFORTaskPlanner.Pages.Login
{
    public class LoginPageModel : PageModel
    {
        private UserService _userService;

        public LoginPageModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPost()
        {
            List<Models.User> users = _userService.GetUsers();
            var tempUser = _userService.GetUserByUsername(Username);
            foreach (Models.User user in users)
            {
                if (user.UserName.ToLower().Equals(Username.ToLower()))
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, tempUser.UserName)
                        };
                        if (user.UserType == Models.User.UserTypes.Admin) claims.Add(new Claim(ClaimTypes.Role, "admin"));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));

                        //Tilføjer cookies, som bruges til at fremvise brugerlisten for brugerens afdeling til at starte med =)
                        Response.Cookies.Append("AssignmentTypeSelect", "0");
                        Response.Cookies.Append("UserSearchDepartment", ((int)tempUser.UserDepartment).ToString());
                        Response.Cookies.Append("DashboardSearchDeparment", ((int)tempUser.UserDepartment).ToString());

                        //smider brugeren videre til Brugerlisten.. skal vi evt sende forskellige stedet alt efter Admin / Bruger?
                        return RedirectToPage("/User/GetUsers");
                    }
                }
            }
            Message = "Invalid attempt";
            return Page();
        }
    }
}
