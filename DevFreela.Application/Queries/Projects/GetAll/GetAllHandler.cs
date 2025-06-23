using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetAll;

public class GetAllHandler : IRequestHandler<GetAllQuery, ResultViewModel<List<GetAllResponse>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<List<GetAllResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _projectRepository
            .GetAllAsync(request.Search, request.Page, request.Size, cancellationToken);

        var model = queryResult.Select(GetAllResponse.FromEntity).ToList();

        return ResultViewModel<List<GetAllResponse>>.Success(model);
    }
}
