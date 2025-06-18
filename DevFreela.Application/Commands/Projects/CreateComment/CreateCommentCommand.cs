using DevFreela.Application.Models.View;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Projects.CreateComment;

public record CreateCommentCommand(
    string Content,
    long UserId
) : IRequest<ResultViewModel>
{
    public long ProjectId { get; private init; }

    public CreateCommentCommand WithProjectId(long id) => this with { ProjectId = id };

    public ProjectComment ToEntity()
        => new(Content, ProjectId, UserId);
}