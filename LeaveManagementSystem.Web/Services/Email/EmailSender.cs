
using System.Net.Mail;

namespace LeaveManagementSystem.Web.Services.Email;

public class EmailSender(IConfiguration _configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromAddress = _configuration["Email:FromAddress"];
        var senderName = _configuration["Email:SenderName"];
        var smtpServer = _configuration["Email:Server"];
        var smtpPort = Convert.ToInt32(_configuration["Email:Port"]);

        var message = new MailMessage
        {
            From = new MailAddress(fromAddress, senderName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
            To = { new MailAddress(email) }
        };

        using var client = new SmtpClient(smtpServer, smtpPort);

        await client.SendMailAsync(message);
    }
}
