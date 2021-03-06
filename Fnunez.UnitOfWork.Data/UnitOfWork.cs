using System.Reflection;
using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Data.Repositories;

namespace Fnunez.UnitOfWork.Data;

public class UnitOfWork : IUnitOfWork
{
    private UnitOfWorkDbContext dbContext;
    #region Repositories
    private CustomerRepository customerRepository;
    private ShippingAddressRepository shippingAddressRepository;
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
        customerRepository = new CustomerRepository(dbContext);
        shippingAddressRepository = new ShippingAddressRepository(dbContext);
    }
}