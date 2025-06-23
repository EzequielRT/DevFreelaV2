using DevFreela.Application.Models.View;
using DevFreela.Application.Settings;
using DevFreela.Core.Repositories;
using Microsoft.Extensions.Options;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;
    private readonly FreelanceTotalCostSettings _freelanceTotalCostSettings;

    public UpdateHandler(IProjectRepository projectRepository, IOptions<FreelanceTotalCostSettings> freelanceTotalCostSettings)
    {
        _projectRepository = projectRepository;
        _freelanceTotalCostSettings = freelanceTotalCostSettings.Value;
    }

    public async Task<ResultViewModel> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        if (request.TotalCost < _freelanceTotalCostSettings.Minimum ||
            request.TotalCost > _freelanceTotalCostSettings.Maximum)
            return ResultViewModel.Error($"O valor deve estar entre {_freelanceTotalCostSettings.Minimum} e {_freelanceTotalCostSettings.Maximum}");

        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (project == null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Update(request.Title, request.Description, request.TotalCost);

        await _projectRepository.UpdateAsync(project);

        return ResultViewModel.Success();
    }
}
