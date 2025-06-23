using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, ResultViewModel<GetByIdResponse>>
{
    private readonly IProjectRepository _projectRepository;

    public GetByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<GetByIdResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetDetailsByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return ResultViewModel<GetByIdResponse>.NotFound("Projeto não encontrado");

        var model = GetByIdResponse.FromEntity(project);

        return ResultViewModel<GetByIdResponse>.Success(model);
    }
}
