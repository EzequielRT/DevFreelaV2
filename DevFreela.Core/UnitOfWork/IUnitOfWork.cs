using DevFreela.Core.Repositories;

namespace DevFreela.Core.UnitOfWork;

public interface IUnitOfWork
{
    IProjectRepository Projects { get; }

    Task CommitAsync();
}
