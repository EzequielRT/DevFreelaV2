using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Delete;

public record DeleteCommand(long Id) : IRequest<ResultViewModel>;