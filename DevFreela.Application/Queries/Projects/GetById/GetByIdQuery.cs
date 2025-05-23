using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetById;

public record GetByIdQuery(long Id) : IRequest<ResultViewModel<GetByIdResponse>>;