using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;

    public DeleteHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (project is null)
            return ResultViewModel<ProjectViewModel>.NotFound("Projeto não encontrado");

        project.SetAsDeleted();

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
