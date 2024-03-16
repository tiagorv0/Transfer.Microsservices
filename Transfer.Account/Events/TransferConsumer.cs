using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Transfer.Account.Domain.Options;
using Transfer.Account.Service;

namespace Transfer.Account.Events;

public class TransferConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private const string QUEUA_NAME = "account";
    private readonly IModel _channel;

    public TransferConsumer(IOptions<RabbitMqOptions> options, IServiceProvider serviceProvider)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = options.Value.Host,
            Port = options.Value.Port,
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
                var transfer = JsonSerializer.Deserialize<TransferEvent>(message);

                await TransferBetweenAccounts(transfer);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: QUEUA_NAME, autoAck: false, consumer: consumer);
    }

    public async Task TransferBetweenAccounts(TransferEvent notificationEvent)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var notificationService = scope.ServiceProvider.GetRequiredService<IAccountService>();

            await notificationService.TransferBetweenAccounts(notificationEvent);
        }
    }
}
