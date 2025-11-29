
using SendGrid.Helpers.Mail;
using SendGrid;

namespace LeaveManagementSystem.Web.Services.EmailClients
{
    public class SendGirdClient(IConfiguration _configuration, ILogger<EmailSender> _logger) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var fromAddress = _configuration["EmailSendGrid:FromAddress"];
                var apiKey = _configuration["EmailSendGrid:APIKey"];
                var senderName = _configuration["EmailSendGrid:SenderName"];

                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(fromAddress, senderName);
                var to = new EmailAddress(email);
                var message = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);

                var response = await client.SendEmailAsync(message);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email sent successfully to {Email}", email);
                }
                else
                {
                    var body = await response.Body.ReadAsStringAsync();
                    _logger.LogError("Failed to send email. Status: {StatusCode}, Body: {Body}",
                        response.StatusCode, body);
                    throw new Exception($"Failed to send email: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", email);
                throw;
            }
        }

    }
}
