using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.Start;

public class StartHandler : IRequestHandler<StartCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;

    public StartHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (project is null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Start();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
