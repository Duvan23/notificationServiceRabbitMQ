using NotificationConsumer.Models;

namespace NotificationConsumer.Services
{
    public interface ISmsNotificationService
    {
        Task SendSmsAsync(NotificationRequest notification);
    }
}
