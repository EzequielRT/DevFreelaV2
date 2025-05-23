using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetById;

public class GetByIdQuery : IRequest<ResultViewModel<GetByIdResponse>>
{
    public GetByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
