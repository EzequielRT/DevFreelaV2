namespace DevFreela.Core.Services;

public interface IPaymentService
{
    Task ProcessPaymentAsync(
        long projectId,
        string creditCardNumber,
        string cvv,
        string expiresAt,
        string fullName,
        decimal amount
    );
}
