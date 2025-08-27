using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Complete;

public record CompleteCommand(
    string CreditCardNumber,
    string Cvv,
    string ExpiresAt,
    string FullName,
    decimal Amount
) : IRequest<ResultViewModel>
{
    public long ProjectId { get; private init; }

    public CompleteCommand WithProjectId(long id) => this with { ProjectId = id };
}

