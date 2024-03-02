using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Transfer.Notification.Domain.Options;
using Transfer.Notification.Service;

namespace Transfer.Notification.Events;

public class TransferNotificationConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private const string QUEUA_NAME = "notification";
    private readonly IModel _channel;

    public TransferNotificationConsumer(IOptions<RabbitMqOptions> options, IServiceProvider serviceProvider)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = options.Value.Host,
        };

        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(
            queue: QUEUA_NAME,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var notification = JsonSerializer.Deserialize<SendNotificationEvent>(message);

                await CreateNotification(notification);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: QUEUA_NAME, autoAck: false, consumer: consumer);
    }

    public async Task CreateNotification(SendNotificationEvent notificationEvent)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            await notificationService.CreateNotification(notificationEvent);
        }
    }
}
