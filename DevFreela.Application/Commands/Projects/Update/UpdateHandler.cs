using DevFreela.Application.Models.View;
using DevFreela.Application.Options;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    private readonly FreelanceTotalCostOptions _freelanceTotalCostOptions;

    public UpdateHandler(DevFreelaDbContext context, IOptions<FreelanceTotalCostOptions> freelanceTotalCostOptions)
    {
        _context = context;
        _freelanceTotalCostOptions = freelanceTotalCostOptions.Value;
    }

    public async Task<ResultViewModel> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        if (request.TotalCost < _freelanceTotalCostOptions.Minimum ||
            request.TotalCost > _freelanceTotalCostOptions.Maximum)
            return ResultViewModel.Error($"O valor deve estar entre {_freelanceTotalCostOptions.Minimum} e {_freelanceTotalCostOptions.Maximum}");

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        if (project == null)
            return ResultViewModel<ProjectViewModel>.NotFound("Projeto não encontrado");

        project.Update(request.Title, request.Description, request.TotalCost);

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
