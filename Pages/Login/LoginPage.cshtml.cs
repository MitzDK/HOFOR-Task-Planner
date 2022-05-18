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
using Microsoft.AspNetCore.Http;
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
        //Hvis log-ind oplysninger er korrekte bliver der tilføjet ClaimTypes.Role hvis brugeren er admin. Uanset hvad bliver det tilføjet cookies for automatisk at kunne filtrere brugerlisten efter log-ind.
        public async Task<IActionResult> OnPost()
        {
            List<Models.User> users = _userService.GetUsers();
            if (Username != null && Password != null)
            {
                foreach (Models.User user in users)
                {
                    if (user.UserName.ToLower().Equals(Username.ToLower()))
                    {
                        var passwordHasher = new PasswordHasher<string>();
                        if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.UserName)
                            };
                            if (user.UserType == Models.User.UserTypes.Admin) claims.Add(new Claim(ClaimTypes.Role, "admin"));
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity));

                            //Tilføjer cookies, som bruges til at fremvise brugerlisten for brugerens afdeling til at starte med =)
                            Response.Cookies.Append("UserSearchDepartment", ((int)user.UserDepartment).ToString());
                            Response.Cookies.Append("DashboardSearchDeparment", ((int)user.UserDepartment).ToString());
                            Response.Cookies.Append("AssignmentIsTypeFiltered", "test", new CookieOptions
                                { Expires = DateTime.Now.AddDays(-1D) }
                            );
                            Response.Cookies.Append("AssignmentFilterTypeValue", "test", new CookieOptions
                                { Expires = DateTime.Now.AddDays(-1D) }
                            );
                            Response.Cookies.Append("AssignmentIsDescriptionFiltered", "test", new CookieOptions
                                { Expires = DateTime.Now.AddDays(-1D) }
                            );
                            Response.Cookies.Append("AssignmentFilterDescriptionValue", "test", new CookieOptions
                                { Expires = DateTime.Now.AddDays(-1D) }
                            );
                            //smider brugeren videre til Brugerlisten.. skal vi evt sende forskellige stedet alt efter Admin / Bruger?
                            return RedirectToPage("/User/GetUsers");
                        }
                    }
                }
            }

            
            Message = "Ugyldigt login";
            return Page();
        }
    }
}
