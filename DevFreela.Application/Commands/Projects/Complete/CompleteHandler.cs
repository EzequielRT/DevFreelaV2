using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Complete;

public class CompleteHandler : IRequestHandler<CompleteCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public CompleteHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(CompleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Complete();

        await _projectRepository.UpdateAsync(project);

        return ResultViewModel.Success();
    }
}