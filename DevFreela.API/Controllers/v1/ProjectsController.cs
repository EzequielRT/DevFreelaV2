using DevFreela.Application.Commands.Projects.Complete;
using DevFreela.Application.Commands.Projects.Create;
using DevFreela.Application.Commands.Projects.CreateComment;
using DevFreela.Application.Commands.Projects.Delete;
using DevFreela.Application.Commands.Projects.Start;
using DevFreela.Application.Commands.Projects.Update;
using DevFreela.Application.Queries.Projects.GetAll;
using DevFreela.Application.Queries.Projects.GetById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers.v1;

public class ProjectsController : BaseApiController
{
    public ProjectsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [Authorize(Roles = "freelancer, client")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllQuery query, CancellationToken cancellationToken) 
        => await SendAsync(query, cancellationToken);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new GetByIdQuery(id), cancellationToken);

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Create([FromBody] CreateCommand command)
        => await SendAsync(command);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateCommand command)
        => await SendAsync(command.WithProjectId(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
        => await SendAsync(new DeleteCommand(id));

    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start([FromRoute] long id)
        => await SendAsync(new StartCommand(id));

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete([FromBody] CompleteCommand command)
        => await SendAsync(command);

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> CreateComment([FromRoute] long id, [FromBody] CreateCommentCommand command)
        => await SendAsync(command.WithProjectId(id));
}
