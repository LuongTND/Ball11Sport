using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService
{
    public interface IMailService
    {
        public Task SendMailAsync(MailContent content);
    
    }
    public class SendMailService : IMailService
    {
        private readonly MailSettings mailSettings;
     
        public SendMailService(IOptions<MailSettings> _mailSettings)
        {
            mailSettings = _mailSettings.Value;

        }
        public async Task SendMailAsync(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);             
            }
            await smtp.DisconnectAsync(true);
        }
    }
}
