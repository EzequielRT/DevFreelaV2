using DevFreela.Payments.API.Entities;
using DevFreela.Payments.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Payments.API.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _context;

    public ProjectRepository(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(long id)
    {
        return await _context.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
}
