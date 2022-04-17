using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Models;

namespace Fnunez.UnitOfWork.Data.Repositories;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(UnitOfWorkDbContext dbContext) : base(dbContext)
    {
    }
}