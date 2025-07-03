using DevFreela.Application.Commands.Projects.Create.Notifications;
using DevFreela.Application.Models.View;
using DevFreela.Application.Settings;
using DevFreela.Core.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace DevFreela.Application.Commands.Projects.Create;

public class CreateHandler : IRequestHandler<CreateCommand, ResultViewModel<CreateResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly FreelanceTotalCostSettings _freelanceTotalCostSettings;
    private readonly IMediator _mediator;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(
        IProjectRepository projectRepository,
        IOptions<FreelanceTotalCostSettings> freelanceTotalCostSettings,
        IMediator mediator,
        IValidator<CreateCommand> validator)
    {
        _projectRepository = projectRepository;
        _freelanceTotalCostSettings = freelanceTotalCostSettings.Value;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<ResultViewModel<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validate = await _validator.ValidateAsync(request);
        if (!validate.IsValid)
            return ResultViewModel<CreateResponse>.Error(validate.Errors.First().ErrorMessage);

        if (request.TotalCost < _freelanceTotalCostSettings.Minimum ||
            request.TotalCost > _freelanceTotalCostSettings.Maximum)
            return ResultViewModel<CreateResponse>.Error($"O valor deve estar entre {_freelanceTotalCostSettings.Minimum} e {_freelanceTotalCostSettings.Maximum}");

        var project = request.ToEntity();

        await _projectRepository.AddAsync(project);

        var projectCreatedNotification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreatedNotification);

        return ResultViewModel<CreateResponse>.Success(new CreateResponse(project.Id));
    }
}
