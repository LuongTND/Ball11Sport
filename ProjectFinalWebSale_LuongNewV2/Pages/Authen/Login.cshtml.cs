using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class LoginModel : PageModel
    {

        private readonly Data.ApplicationDBContext _context;

        public LoginModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var acc = _context.Users
            //.Where(a => a.Email == User.Email && a.Password == User.Password)
            //.FirstOrDefault();
            var acc =await _context.Users.FirstOrDefaultAsync(acc => acc.UserName.Equals(User.UserName) && User.Password.Equals(User.Password));

            if (acc != null)
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, acc.UserName),
                new Claim("UserName", acc.UserName),
                new Claim(ClaimTypes.Email, acc.Email), };


                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties  { };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait(); // Wait for the asynchronous operation to complete
                int userId = Convert.ToInt32(acc.UserId);
                HttpContext.Session.SetInt32("UserID", userId);

                return RedirectToPage("/Index");
            }
            else
            {
          
                ViewData["LoginStatus"] = 0;
          
                ViewData["msg"] = "Username and Password combination is incorrect!";
                return Page();
            }
            
        }
    }
}
