using System.Reflection;
using Fnunez.UnitOfWork.Data.Abstractions;

namespace Fnunez.UnitOfWork.Data;

public class UnitOfWork : IUnitOfWork
{
    private UnitOfWorkDbContext dbContext;
    #region Repositories
    #endregion

    public UnitOfWork(UnitOfWorkDbContext dbContext)
    {
        this.dbContext = dbContext;
        StartupRepositories();
    }

    public async void Dispose()
    {
        await dbContext.DisposeAsync();
    }

    public R GetRepository<R>()
    {
        return (R)GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .SingleOrDefault(x => x.FieldType == typeof(R) || typeof(R)
            .IsAssignableFrom(x.FieldType))
            .GetValue(this);
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    private void StartupRepositories()
    {
    }
}