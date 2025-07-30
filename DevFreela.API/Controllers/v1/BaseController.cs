using DevFreela.Application.Models.View;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers.v1;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> SendAsync<TResponse>(IRequest<ResultViewModel<TResponse>> request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }

    protected async Task<IActionResult> SendAsync(IRequest<ResultViewModel> request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }
}
