namespace DevFreela.Infra.Payments;

public record PaymentInfoInputModel(
    long ProjectId,
    string CreditCardNumber,
    string Cvv,
    string ExpiresAt,
    string FullName,
    decimal Amount
);