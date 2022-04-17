using Microsoft.EntityFrameworkCore;

namespace Fnunez.UnitOfWork.Data;

public class UnitOfWorkDbContext : DbContext
{
    public string ConnectionString { get; set; }
    #region DbSets
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
    }
}