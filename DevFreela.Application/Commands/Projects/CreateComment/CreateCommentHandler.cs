using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects.CreateComment;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public CreateCommentHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _projectRepository.ExistsAsync(request.ProjectId);

        if (!exists)
            return ResultViewModel.NotFound("Projeto não encontrado");

        var comment = request.ToEntity();

        await _projectRepository.AddCommentAsync(comment);
        
        return ResultViewModel.Success();
    }
}