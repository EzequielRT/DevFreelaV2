using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Projects.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, ResultViewModel<GetByIdResponse>>
{
    private readonly DevFreelaDbContext _context;

    public GetByIdHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<GetByIdResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project is null)
            return ResultViewModel<GetByIdResponse>.NotFound("Projeto não encontrado");

        var model = GetByIdResponse.FromEntity(project);

        return ResultViewModel<GetByIdResponse>.Success(model);
    }
}
