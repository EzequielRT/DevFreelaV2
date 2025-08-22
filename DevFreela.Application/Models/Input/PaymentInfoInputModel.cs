namespace DevFreela.Application.Models.Input;

public record PaymentInfoInputModel(
    long ProjectId,
    string CreditCardNumber,
    string Cvv,
    string ExpiresAt,
    string FullName,
    decimal Amount
);