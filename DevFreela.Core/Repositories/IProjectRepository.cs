using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync(string? search = null, int page = 0, int size = 10, CancellationToken cancellationToken = default);
    Task<Project?> GetDetailsByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(long id);
    Task<long> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task AddCommentAsync(ProjectComment comment);
    Task<bool> ExistsAsync(long id);
}
