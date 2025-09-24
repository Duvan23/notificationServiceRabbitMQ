using NotificationConsumer.Models;
using System.Net;
using System.Net.Mail;

namespace NotificationConsumer.Services
{
    public class EmailNotificationService : BaseNotificationConsumerService, IEmailNotificationService
    {
        public EmailNotificationService(ILogger<EmailNotificationService> logger)
            : base(logger, "email.notification.queue", "notification.email")
        {
        }

        protected override async Task ProcessNotificationAsync(NotificationRequest notification)
        {
            await SendEmailAsync(notification);
        }

        public async Task SendEmailAsync(NotificationRequest notification)
        {
            try
            {
                _logger.LogInformation("Processing email notification for {Recipient}", notification.Recipient);

                // Simulate email sending delay
                await Task.Delay(1000);

                using var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("test@gmail.com", "key"),
                    EnableSsl = true
                };

                var mail = new MailMessage(
                    from: "test60@gmail.com",
                    to: notification.Recipient,
                    subject: notification.Subject ?? "(sin asunto)",
                    body: notification.Message ?? string.Empty
                );

                await client.SendMailAsync(mail);

                // Here you would integrate with your actual email service (SendGrid, SMTP, etc.)
                _logger.LogInformation("EMAIL SENT: To={Recipient}, Subject={Subject}, Message={Message}",
                    notification.Recipient, notification.Subject, notification.Message);

                // You could also store the result in a database, send to monitoring, etc.
                _logger.LogInformation("Email notification sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email notification");
                throw; // Re-throw to trigger message requeue
            }
        }
    }
}
