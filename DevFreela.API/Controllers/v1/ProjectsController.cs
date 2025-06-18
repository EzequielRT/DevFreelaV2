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

namespace DevFreela.API.Controllers.v1;

public class ProjectsController : BaseApiController
{
    public ProjectsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllQuery query, CancellationToken cancellationToken) 
        => await SendAsync(query, cancellationToken);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new GetByIdQuery(id), cancellationToken);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommand command, CancellationToken cancellationToken)
        => await SendAsync(command, cancellationToken);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateCommand command, CancellationToken cancellationToken)
        => await SendAsync(command.WithProjectId(id), cancellationToken);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new DeleteCommand(id), cancellationToken);

    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new StartCommand(id), cancellationToken);

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete([FromRoute] long id, CancellationToken cancellationToken)
        => await SendAsync(new CompleteCommand(id), cancellationToken);

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> CreateComment([FromRoute] long id, [FromBody] CreateCommentCommand command, CancellationToken cancellationToken)
        => await SendAsync(command.WithProjectId(id), cancellationToken);
}
