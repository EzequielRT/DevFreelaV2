namespace DevFreela.Core.Services;

public interface IMessageBusService
{
    Task PublishAsync(string queue, byte[] message);
}
