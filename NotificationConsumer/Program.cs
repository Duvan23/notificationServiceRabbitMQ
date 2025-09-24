
using NotificationConsumer.Services;

namespace NotificationConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register notification services
            builder.Services.AddSingleton<EmailNotificationService>();
            builder.Services.AddSingleton<SmsNotificationService>();


            // Register services for DI
            builder.Services.AddScoped<IEmailNotificationService>(provider =>
                provider.GetRequiredService<EmailNotificationService>());
            builder.Services.AddScoped<ISmsNotificationService>(provider =>
                provider.GetRequiredService<SmsNotificationService>());

            // Register background services
            builder.Services.AddHostedService<EmailNotificationBackgroundService>();
            builder.Services.AddHostedService<SmsNotificationBackgroundService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
