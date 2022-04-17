namespace Fnunez.UnitOfWork.Data.Abstractions;

public interface IUnitOfWork : IDisposable
{
    R GetRepository<R>();
    void SaveChanges();
    Task SaveChangesAsync();
}