using DevFreela.Payments.API.Entities;

namespace DevFreela.Payments.API.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(long id);
    Task UpdateAsync(Project project);
}