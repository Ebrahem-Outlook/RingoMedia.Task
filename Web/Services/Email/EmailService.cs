using System.Net.Mail;

namespace Web.Services.Email;

internal sealed class EmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage("outlook.ebrahem@gmail.com", to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };

        using(var smtpClient = new SmtpClient("smtp.example.com"))
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
