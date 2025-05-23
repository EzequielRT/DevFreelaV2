using DevFreela.Application.Models.View;
using DevFreela.Application.Options;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace DevFreela.Application.Commands.Projects.Create;

public class CreateHandler : IRequestHandler<CreateCommand, ResultViewModel<CreateResponse>>
{
    private readonly DevFreelaDbContext _context;
    private readonly FreelanceTotalCostOptions _freelanceTotalCostOptions;

    public CreateHandler(DevFreelaDbContext context, IOptions<FreelanceTotalCostOptions> freelanceTotalCostOptions)
    {
        _context = context;
        _freelanceTotalCostOptions = freelanceTotalCostOptions.Value;
    }

    public async Task<ResultViewModel<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if (request.TotalCost < _freelanceTotalCostOptions.Minimum ||
            request.TotalCost > _freelanceTotalCostOptions.Maximum)
            return ResultViewModel<CreateResponse>.Error($"O valor deve estar entre {_freelanceTotalCostOptions.Minimum} e {_freelanceTotalCostOptions.Maximum}");

        var project = request.ToEntity();

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return ResultViewModel<CreateResponse>.Success(new CreateResponse(project.Id));
    }
}
