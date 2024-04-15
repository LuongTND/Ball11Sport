using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class LogoutModel : PageModel
    {
        
        public IActionResult OnGet()
        {
            HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}