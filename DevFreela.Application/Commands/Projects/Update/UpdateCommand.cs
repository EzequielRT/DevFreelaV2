using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Update;

public record UpdateCommand(
    string Title,
    string Description,
    decimal TotalCost
) : IRequest<ResultViewModel>
{
    public long ProjectId { get; private init; }

    public UpdateCommand WithProjectId(long id) => this with { ProjectId = id };
}