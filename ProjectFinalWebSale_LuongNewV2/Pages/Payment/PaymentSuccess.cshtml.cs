using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using System.Security.Claims;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Payment
{
    public class PaymentSuccessModel : PageModel
    {
        
        private readonly IMailService _mailService;

        private readonly Data.ApplicationDBContext _context;



        public string Message { get; set; }

        public PaymentSuccessModel(Data.ApplicationDBContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;

        }
        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            //var user = userIdClaim;

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                Order.OrderDate = DateTime.Now;
                Order.UserId = userId;
                Order.Status = true;

                _context.Orders.Add(Order);
                await _context.SaveChangesAsync();


            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                Message = "User email not found.";
                return Page();
            }

            var mailContent = new MailContent
            {
                To = userEmail,
                Subject = "Đã Thanh Toán Thành Công",
                Body = "Cảm ơn đã mua hàng 🥰 "
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
