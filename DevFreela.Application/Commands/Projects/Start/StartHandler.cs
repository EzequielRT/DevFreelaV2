using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Start;

public class StartHandler : IRequestHandler<StartCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public StartHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Start();

        await _projectRepository.UpdateAsync(project);

        return ResultViewModel.Success();
    }
}
