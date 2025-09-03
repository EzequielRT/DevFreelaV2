using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _context;

    public ProjectRepository(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project.Id;
    }

    public async Task AddCommentAsync(ProjectComment comment)
    {
        await _context.ProjectComments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Projects
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }

    public async Task<(List<Project>, int)> GetAllAsync(string? search = null, int page = 0, int size = 10, CancellationToken cancellationToken = default)
    {
        var query = _context.Projects
            .AsNoTracking()
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => p.DeletedAt == null)            
            .AsQueryable();

        search = search?.Trim();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query
                .Where(p => p.Title.Contains(search) ||
                            p.Description.Contains(search));
        }

        var count = await query.CountAsync();

        var queryResult = await query
            .OrderBy(p => p.Title)
            .Skip(page * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (queryResult, count);
    }

    public async Task<Project?> GetByIdAsync(long id)
    {
        return await _context.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Project?> GetDetailsByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return project;
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
}
