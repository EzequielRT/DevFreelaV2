using DevFreela.Core.Repositories;
using DevFreela.Core.UnitOfWork;

namespace DevFreela.Infra.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DevFreelaDbContext _context;

    public UnitOfWork(DevFreelaDbContext context, IProjectRepository projects)
    {
        _context = context;
        Projects = projects;
    }

    public IProjectRepository Projects { get; }

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();
    
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _context.Dispose();

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
