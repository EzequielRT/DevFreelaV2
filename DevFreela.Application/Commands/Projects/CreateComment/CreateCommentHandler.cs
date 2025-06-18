using DevFreela.Application.Models.View;
using DevFreela.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Projects.CreateComment;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;

    public CreateCommentHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        if (project is null)
            return ResultViewModel<ProjectViewModel>.NotFound("Projeto não encontrado");

        var comment = request.ToEntity();

        await _context.ProjectComments.AddAsync(comment);
        await _context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}