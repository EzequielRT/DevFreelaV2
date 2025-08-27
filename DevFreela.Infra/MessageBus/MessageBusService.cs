using DevFreela.Core.Services;
using RabbitMQ.Client;

namespace DevFreela.Infra.MessageBus;

public class MessageBusService : IMessageBusService
{
    private readonly IConnectionFactory _factory;

    public MessageBusService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin"
        };
    }

    public async Task PublishAsync(string queue, byte[] message)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // Garante que a fila esteja criada
        await channel.QueueDeclareAsync(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Publicar a mensagem
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queue,
            body: message
        );
    }
}
