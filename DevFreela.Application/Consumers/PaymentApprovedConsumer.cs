using DevFreela.Core.IntegrationEvents;
using DevFreela.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DevFreela.Application.Consumers;

public class PaymentApprovedConsumer : BackgroundService
{
    private const string PAYMENT_APPROVED_QUEUE_NAME = "payments-approved";

    private readonly IServiceProvider _serviceProvider;
    private readonly IConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;

    public PaymentApprovedConsumer(IServiceProvider serviceProvider)
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

        await DeclareQueue(PAYMENT_APPROVED_QUEUE_NAME);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var paymentApprovedIntegrationEvent = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(json);

            if (paymentApprovedIntegrationEvent is null) return;

            await ProcessPaymentApprovedEventAsync(paymentApprovedIntegrationEvent);
            await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        };

        await _channel.BasicConsumeAsync(PAYMENT_APPROVED_QUEUE_NAME, autoAck: false, consumer);
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

    private async Task ProcessPaymentApprovedEventAsync(PaymentApprovedIntegrationEvent paymentApprovedIntegrationEvent)
    {
        using var scope = _serviceProvider.CreateScope();
        var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

        var project = await projectRepository.GetByIdAsync(paymentApprovedIntegrationEvent.ProjectId);

        project!.Complete();

        await projectRepository.UpdateAsync(project);
    }
}
