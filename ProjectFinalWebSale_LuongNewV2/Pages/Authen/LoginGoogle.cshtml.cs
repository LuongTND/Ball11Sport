using Data;
using MailKit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;


namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class LoginGoogleModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public string Message { get; set; }

        public LoginGoogleModel(ApplicationDBContext context)
        {
            _context = context;
           
        }
      
        [BindProperty]
        public User User { get; set; }

        public async Task OnGet()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
                {
                RedirectUri = "/Authen/Welcome"
            });
        }

        

    }
}



