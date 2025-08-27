using DevFreela.Payments.API.Models;

namespace DevFreela.Payments.API.Services;

public class PaymentService : IPaymentService
{
    public async Task<bool> ProcessAsync(PaymentInfoInputModel model)
    {
        return await Task.FromResult(true);
    }
}
