using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infra.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMessageBusService _messageBusService;
    private const string QUEUE_NAME = "payments";

    public PaymentService(IMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public async Task ProcessPaymentAsync(
        long projectId, 
        string creditCardNumber,
        string cvv,
        string expiresAt,
        string fullName,
        decimal amount)
    {
        var model = new PaymentInfoInputModel(projectId, creditCardNumber, cvv,expiresAt, fullName, amount);
        var paymentInfoJson = JsonSerializer.Serialize(model);
        var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
        await _messageBusService.PublishAsync(QUEUE_NAME, paymentInfoBytes);
    }
}
