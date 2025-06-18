using DevFreela.Application.Models.View;
using MediatR;

namespace DevFreela.Application.Commands.Projects.Start;

public record StartCommand(long Id) : IRequest<ResultViewModel>;
