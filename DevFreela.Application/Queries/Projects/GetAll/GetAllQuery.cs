using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetAll;

public record GetAllQuery(string? Search = null, int Page = 0, int Size = 10) : IRequest<ResultViewModel<List<GetAllResponse>>>;
