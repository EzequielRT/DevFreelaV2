namespace DevFreela.Infra.Payments;

public record PaymentModel(
    long ProjectId,
    string CreditCardNumber,
    string Cvv,
    string ExpiresAt,
    string FullName,
    decimal Amount
);