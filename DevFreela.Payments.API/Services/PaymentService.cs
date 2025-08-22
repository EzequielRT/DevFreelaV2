using DevFreela.Payments.API.Models;

namespace DevFreela.Payments.API.Services;

public interface IPaymentService
{
    Task<bool> Process(PaymentInfoInputModel model);
}

public class PaymentService : IPaymentService
{
    public Task<bool> Process(PaymentInfoInputModel model)
    {
        return Task.FromResult(true);
    }
}
