using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                Console.WriteLine($"📨 [DEBUG] Sending email to: {toEmail}");

                var emailSettings = _configuration.GetSection("EmailSettings");

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Pregnancy Growth Tracking", emailSettings["SenderEmail"]));
                email.To.Add(new MailboxAddress("", toEmail));
                email.Subject = subject;
                email.Body = new TextPart("html") { Text = body };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                Console.WriteLine($"✅ [SUCCESS] Email sent successfully to {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [ERROR] Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}
