using DevFreela.Application.Commands.Projects.Create.Notifications;
using DevFreela.Application.Models.View;
using DevFreela.Application.Settings;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace DevFreela.Application.Commands.Projects.Create;

public class CreateHandler : IRequestHandler<CreateCommand, ResultViewModel<CreateResponse>>
{
    private readonly DevFreelaDbContext _context;
    private readonly FreelanceTotalCostSettings _freelanceTotalCostSettings;
    private readonly IMediator _mediator;

    public CreateHandler(DevFreelaDbContext context, IOptions<FreelanceTotalCostSettings> freelanceTotalCostSettings, IMediator mediator)
    {
        _context = context;
        _freelanceTotalCostSettings = freelanceTotalCostSettings.Value;
        _mediator = mediator;
    }

    public async Task<ResultViewModel<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if (request.TotalCost < _freelanceTotalCostSettings.Minimum ||
            request.TotalCost > _freelanceTotalCostSettings.Maximum)
            return ResultViewModel<CreateResponse>.Error($"O valor deve estar entre {_freelanceTotalCostSettings.Minimum} e {_freelanceTotalCostSettings.Maximum}");

        var project = request.ToEntity();

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        var projectCreatedNotification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreatedNotification);

        return ResultViewModel<CreateResponse>.Success(new CreateResponse(project.Id));
    }
}
