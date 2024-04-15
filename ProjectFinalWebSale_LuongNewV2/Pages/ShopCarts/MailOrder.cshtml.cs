using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;
using ProjectFinalWebSale_LuongNewV2.Pages.Authen;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using System.Security.Claims;

namespace ProjectFinalWebSale_LuongNewV2.Pages.ShopCarts
{
    public class MailOrderModel : PageModel
    {
       
            private readonly IMailService _mailService;

        private readonly Data.ApplicationDBContext _context;



        public string Message { get; set; }

        public MailOrderModel(Data.ApplicationDBContext context,IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
      
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            //var user = userIdClaim;


            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                Message = "User email not found.";
                return Page();
            }

            var mailContent = new MailContent
            {
                To = userEmail,
                Subject = "Cảm ơn đã mua hàng 🥰",
                Body = "Ting ting ting ... "
            };

            try
            {
                await _mailService.SendMailAsync(mailContent);
                Message = "Email sent successfully!";
                return RedirectToPage("/Index");
                //return Page();
            }
            catch (System.Exception ex)
            {
             
                Message = "Failed to send email. Please try again later.";
            }

            HttpContext.Session.Remove("GioHang");


            return RedirectToPage("/Index");
            //return Page();
        }
    }
}
