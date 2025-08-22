namespace DevFreela.Infra.Payments;

public interface IPaymentService
{
    Task<bool> ProcessPayment(PaymentModel model);
}
