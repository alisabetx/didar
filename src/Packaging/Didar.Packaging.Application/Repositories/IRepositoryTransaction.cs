namespace Didar.Packaging.Application.Repositories;

public interface IRepositoryTransaction : IDisposable
{
    void Commit();
    void Rollback();
}
