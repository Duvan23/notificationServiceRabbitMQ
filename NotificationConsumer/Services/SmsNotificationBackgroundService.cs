namespace NotificationConsumer.Services
{
    public class SmsNotificationBackgroundService : BackgroundService
    {
        private readonly SmsNotificationService _smsService;
        private readonly ILogger<SmsNotificationBackgroundService> _logger;

        public SmsNotificationBackgroundService(
            SmsNotificationService smsService,
            ILogger<SmsNotificationBackgroundService> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SMS Notification Background Service started");

            try
            {
                await _smsService.StartAsync(stoppingToken);

                // Keep alive
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("SMS Notification Background Service was cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMS Notification Background Service encountered an error");
            }
            finally
            {
                await _smsService.StopAsync(CancellationToken.None);
                _logger.LogInformation("SMS Notification Background Service stopped");
            }
        }
    }
}
