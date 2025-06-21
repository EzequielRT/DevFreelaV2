using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.Complete;

public class CompleteHandler : IRequestHandler<CompleteCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;

    public CompleteHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(CompleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (project is null)
            return ResultViewModel.NotFound("Projeto não encontrado");

        project.Complete();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}