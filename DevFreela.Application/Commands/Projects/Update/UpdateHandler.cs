using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MediatR;
using DevFreela.Application.Settings;

namespace DevFreela.Application.Commands.Projects.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    private readonly FreelanceTotalCostSettings _freelanceTotalCostSettings;

    public UpdateHandler(DevFreelaDbContext context, IOptions<FreelanceTotalCostSettings> freelanceTotalCostSettings)
    {
        _context = context;
        _freelanceTotalCostSettings = freelanceTotalCostSettings.Value;
    }

    public async Task<ResultViewModel> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        if (request.TotalCost < _freelanceTotalCostSettings.Minimum ||
            request.TotalCost > _freelanceTotalCostSettings.Maximum)
            return ResultViewModel.Error($"O valor deve estar entre {_freelanceTotalCostSettings.Minimum} e {_freelanceTotalCostSettings.Maximum}");

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        if (project == null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Update(request.Title, request.Description, request.TotalCost);

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
