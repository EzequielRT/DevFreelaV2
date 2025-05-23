using DevFreela.Application.Models.View;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Create;

public record CreateCommand(
    string Title,
    string Description,
    long ClientId,
    long FreelancerId,
    decimal TotalCost
) : IRequest<ResultViewModel<CreateResponse>>
{
    public Project ToEntity() => new(Title, Description, ClientId, FreelancerId, TotalCost);
}
