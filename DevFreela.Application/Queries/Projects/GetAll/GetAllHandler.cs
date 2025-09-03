using DevFreela.Application.Models.View;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetAll;

public class GetAllHandler : IRequestHandler<GetAllQuery, ResultViewModel<GetAllResponse>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<GetAllResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _projectRepository
            .GetAllAsync(request.Search, request.Page, request.Size, cancellationToken);

        var projects = queryResult.Item1.Select(GetAllItemResponse.FromEntity).ToList();
        var count = queryResult.Item2;
        var model = new GetAllResponse(request.Page, request.Size, count, projects);

        return ResultViewModel<GetAllResponse>.Success(model);
    }
}
