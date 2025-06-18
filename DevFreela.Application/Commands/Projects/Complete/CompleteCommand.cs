using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Complete;

public record CompleteCommand(long Id) : IRequest<ResultViewModel>;
