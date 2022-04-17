using Fnunez.UnitOfWork.Data.Mappings;
using Fnunez.UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.UnitOfWork.Data;

public class UnitOfWorkDbContext : DbContext
{
    public string ConnectionString { get; set; }
    #region DbSets
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    #endregion

    public UnitOfWorkDbContext(DbContextOptions<UnitOfWorkDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        if (!dbContextOptionsBuilder.IsConfigured)
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerMapping());
        modelBuilder.ApplyConfiguration(new ShippingAddressMapping());
    }
}