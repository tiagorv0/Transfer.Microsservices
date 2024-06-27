
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Transfer.Api.Domain.Options;

namespace Transfer.Api.Event;

public class EventBus : IEventBus
{
    private readonly RabbitMqOptions _options;

    public EventBus(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
    }

    public void Publish<T>(T message, string queue) 
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        using var connection = connectionFactory.CreateConnection();
        using (var channel = connection.CreateModel())
        {

            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = JsonSerializer.Serialize(message);

            var bytes = Encoding.UTF8.GetBytes(body);

            channel.BasicPublish("", queue, null, bytes);
        }
    }
}
