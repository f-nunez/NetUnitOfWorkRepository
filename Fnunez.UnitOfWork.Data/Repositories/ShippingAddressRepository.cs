using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Models;

namespace Fnunez.UnitOfWork.Data.Repositories;

public class ShippingAddressRepository : GenericRepository<ShippingAddress>
{
    public ShippingAddressRepository(UnitOfWorkDbContext dbContext) : base(dbContext)
    {
    }
}