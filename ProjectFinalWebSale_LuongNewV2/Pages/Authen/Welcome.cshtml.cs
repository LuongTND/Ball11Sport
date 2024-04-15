using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Claims;
using Data;
using MailKit;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using System.Security.Claims;
using IMailService = ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService.IMailService;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class WelcomeModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        private readonly IMailService _mailService;
      
        public string Message { get; set; }

        public WelcomeModel(Data.ApplicationDBContext context , IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
       
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result.Principal;

            var emailClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (emailClaim?.Value != null)
            {
                
          
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailClaim.Value);

                if (existingUser == null)
                {

                    existingUser = new User
                    {
                        UserName = claims.FindFirstValue(ClaimTypes.Name),
                        RoleId = 2,
                        Email = claims.FindFirstValue(ClaimTypes.Email)
                    };

                    _context.Users.Add(existingUser);
                    await _context.SaveChangesAsync();
                    var user = User.Identity as ClaimsIdentity;


                 
                    var mailContent = new MailContent
                    {
                        To = emailClaim.Value,
                        Subject = "Cảm ơn ⚽",
                        Body = "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi! 🥰"
                    };

                    try
                    {
                      await _mailService.SendMailAsync(mailContent);
                        Message = "Email sent successfully!";
                    }
                    catch (System.Exception ex)
                    {
                      
                        Message = "Failed to send email. Please try again later.";
                    }
                }
                

                List<Claim> claim = new List<Claim>()
                {
                    new Claim("UserId", existingUser.UserId.ToString()),
                    new Claim(ClaimTypes.Role, existingUser.RoleId.ToString()),
                     new Claim(ClaimTypes.Name,existingUser.UserName),
                     new Claim(ClaimTypes.Email,existingUser.Email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            

            return RedirectToPage("/Index");
        }

    }
}

