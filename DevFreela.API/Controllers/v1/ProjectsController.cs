using DevFreela.Application.Commands.Projects.Create;
using DevFreela.Application.Commands.Projects.Update;
using DevFreela.Application.Models.Input;
using DevFreela.Application.Models.View;
using DevFreela.Application.Queries.Projects.GetAll;
using DevFreela.Application.Queries.Projects.GetById;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Commands.Projects.Delete;

namespace DevFreela.API.Controllers.v1;

public class ProjectsController : BaseApiController
{
    private readonly IProjectService _projectService;

    public ProjectsController(
        IProjectService projectService,
        IMediator mediator) : base(mediator)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllQuery query, CancellationToken cancellationToken) 
        => await SendAsync(query, cancellationToken);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new GetByIdQuery(id), cancellationToken);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCommand command, CancellationToken cancellationToken)
        => await SendAsync(command, cancellationToken);

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UpdateCommand command, CancellationToken cancellationToken)
        => await SendAsync(command.WithProjectId(id), cancellationToken);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new DeleteCommand(id), cancellationToken);

    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start(long id)
    {
        var result = await _projectService.StartAsync(id);

        return result.ToActionResult();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(long id)
    {
        var result = await _projectService.CompleteAsync(id);

        return result.ToActionResult();
    }

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> PostComment(long id, CreateProjectCommentInputModel input)
    {
        input.SetProjectId(id);

        var result = await _projectService.InsertCommentAsync(input);

        return result.ToActionResult();
    }
}
