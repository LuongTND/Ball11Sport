using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using System.Security.Claims;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class SendEmailModel : PageModel
    {
        private readonly IMailService _mailService;
        private readonly ILogger<SendEmailModel> _logger;

        public string Message { get; set; }
        public UserDetail UserDetail { get; set; } = default!;
        public SendEmailModel(IMailService mailService, ILogger<SendEmailModel> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = User.Identity as ClaimsIdentity;
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                Message = "User email not found.";
                return Page();
            }

            var mailContent = new MailContent
            {
                To = userEmail,
                Subject = "Cảm ơn",
                Body = "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!"
            };

            try
            {
                await _mailService.SendMailAsync(mailContent);
                Message = "Email sent successfully!";
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to send email");
                Message = "Failed to send email. Please try again later.";
            }

            return Page();
        }
    }
}
