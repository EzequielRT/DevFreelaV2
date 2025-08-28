using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DevFreela.Payments.API.Consumers;

public class ProcessPaymentConsumer : BackgroundService
{
    private const string QUEUE_NAME = "payments";
    private const string PAYMENT_APPROVED_QUEUE_NAME = "payments-approved";

    private readonly IServiceProvider _serviceProvider;
    private readonly IConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;

    public ProcessPaymentConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin"
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await DeclareQueue(QUEUE_NAME);
        await DeclareQueue(PAYMENT_APPROVED_QUEUE_NAME);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var paymentInfo = JsonSerializer.Deserialize<PaymentInfoInputModel>(json);

            if (paymentInfo is null) return;

            await ProcessPaymentAsync(paymentInfo);
            await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        };

        await _channel.BasicConsumeAsync(QUEUE_NAME, autoAck: false, consumer);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
            await _channel.DisposeAsync();

        if (_connection != null)
            await _connection.DisposeAsync();

        await base.StopAsync(cancellationToken);
    }

    private async Task DeclareQueue(string queueName)
    {
        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    private async Task PublishPaymentApprovedEvent(long projectId)
    {
        var integrationEvent = new PaymentApprovedIntegrationEvent(projectId);
        var json = JsonSerializer.Serialize(integrationEvent);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: PAYMENT_APPROVED_QUEUE_NAME,
            body: body
        );
    }

    private async Task ProcessPaymentAsync(PaymentInfoInputModel paymentInfo)
    {
        using var scope = _serviceProvider.CreateScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

        if (await paymentService.ProcessAsync(paymentInfo))
            await PublishPaymentApprovedEvent(paymentInfo.ProjectId);
    }
}
