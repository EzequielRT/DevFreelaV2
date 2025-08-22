using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Complete;

public record CompleteCommand(
    long ProjectId,
    string CreditCardNumber,
    string Cvv,
    string ExpiresAt,
    string FullName,
    decimal Amount
) : IRequest<ResultViewModel>;

