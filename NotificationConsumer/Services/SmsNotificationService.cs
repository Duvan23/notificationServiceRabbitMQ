using NotificationConsumer.Models;

namespace NotificationConsumer.Services
{
    public class SmsNotificationService : BaseNotificationConsumerService, ISmsNotificationService
    {
        public SmsNotificationService(ILogger<SmsNotificationService> logger)
            : base(logger, "sms.notification.queue", "notification.sms")
        {
        }
        protected override async Task ProcessNotificationAsync(NotificationRequest notification)
        {
            await SendSmsAsync(notification);
        }
        public async Task SendSmsAsync(NotificationRequest notification)
        {
            try
            {
                _logger.LogInformation("Processing SMS notification for {Recipient}", notification.Recipient);

                // Simular el envío (ejemplo con delay)
                await Task.Delay(1000);

                // En un escenario real aquí va la integración con Twilio, Nexmo, etc.
                _logger.LogInformation("SMS SENT: To={Recipient}, Message={Message}",
                    notification.Recipient, notification.Message);

                _logger.LogInformation("SMS notification sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SMS notification");
                throw; // forzar requeue si falla
            }
        }
    }
}
