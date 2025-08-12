namespace Didar.Application.Repositories;

public interface IRepositoryTransaction : IDisposable
{
    void Commit();
    void Rollback();
}
