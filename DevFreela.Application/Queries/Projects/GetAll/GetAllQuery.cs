using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetAll;

public class GetAllQuery : IRequest<ResultViewModel<List<GetAllResponse>>>
{
    public string? Search { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 10;
}
