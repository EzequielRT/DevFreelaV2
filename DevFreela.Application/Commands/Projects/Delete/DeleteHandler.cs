using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, ResultViewModel>
{
    public const string PROJECT_NOT_FOUND_MESSAGE =
        "Projeto não encontrado";

    private readonly IProjectRepository _projectRepository;

    public DeleteHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null)
            return ResultViewModel.NotFound(PROJECT_NOT_FOUND_MESSAGE);

        project.SetAsDeleted();

        await _projectRepository.UpdateAsync(project);

        return ResultViewModel.Success();
    }
}
