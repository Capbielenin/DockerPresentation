using System.Text;
using System.Text.Json;
using KebabOrderApplication.Constants;
using KebabOrderApplication.Events;
using KebabOrderApplication.Models;
using KebabOrderApplication.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace KebabOrderApplication.Services;

class BusReceiver : BackgroundService
{
    private IOptions<RabbitMqConnection> _options;
    private readonly IMessageHandler _messageHandler;
    private bool _isInitialized = false;
    private readonly ILogger<BusReceiver> _logger;
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    
    public BusReceiver(
        ILogger<BusReceiver> logger,
        IOptions<RabbitMqConnection> options,
        IMessageHandler messageHandler)
    {
        _options = options;
        _messageHandler = messageHandler;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_isInitialized)
            await Initialize(stoppingToken);
        try
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _messageHandler.HandleMessage(message);
            };
            _channel.BasicConsume(queue: MessageBusConstants.QUEUE_NAME,
                autoAck: true,
                consumer: consumer);
        }
        catch (Exception e)
        {
            _logger?.LogError(e.Message);
        }

        
        await Task.CompletedTask;
        
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await Initialize(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogInformation("RabbitMQ connection is closed.");    
    }
    
        
    private async Task Initialize(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initialize rabbitmq listener");
        if(_isInitialized)
            await base.StartAsync(cancellationToken);
        _connectionFactory = _options.Value.CreateFactory();

        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(MessageBusConstants.EXCHANGE_NAME, "direct");
        _channel.QueueDeclare(queue: MessageBusConstants.QUEUE_NAME,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _channel.QueueBind(
            MessageBusConstants.QUEUE_NAME, MessageBusConstants.EXCHANGE_NAME, MessageBusConstants.QUEUE_NAME);

        _isInitialized = true;

        await base.StartAsync(cancellationToken);
    }
}