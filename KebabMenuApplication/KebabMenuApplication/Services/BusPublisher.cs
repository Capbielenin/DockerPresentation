using System.Text;
using System.Text.Json;
using KebabMenuApplication.Constants;
using KebabMenuApplication.Options;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace KebabMenuApplication.Services;

public class BusPublisher : IBusPublisher
{
    private readonly ILogger<BusPublisher> _logger;
    private readonly IOptions<RabbitMqConnection> _connection;

    public BusPublisher(
        ILogger<BusPublisher> logger,
        IOptions<RabbitMqConnection> connection)
    {
        _logger = logger;
        _connection = connection;
    }
    
    public void PublishAsync(object message)
    {
        try
        {
            _logger.LogInformation($"Publishing message {message.ToString()}");
            var factory = _connection.Value.CreateFactory();

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(MessageBusConstants.EXCHANGE_NAME, "direct");
            channel.QueueDeclare(MessageBusConstants.QUEUE_NAME,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(MessageBusConstants.QUEUE_NAME, MessageBusConstants.EXCHANGE_NAME, MessageBusConstants.QUEUE_NAME);
            
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish(exchange:MessageBusConstants.EXCHANGE_NAME,
                routingKey: MessageBusConstants.QUEUE_NAME,
                basicProperties: null,
                body: body);
            _logger.LogInformation($"Finish publish  message {message.ToString()}");

        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error during connecting to RabbitMq");
            throw;
        }
        
    }
}