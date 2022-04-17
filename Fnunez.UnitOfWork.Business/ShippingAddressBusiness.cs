using Fnunez.UnitOfWork.Business.Abstractions;
using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Data.Repositories;
using Fnunez.UnitOfWork.Models;

namespace Fnunez.UnitOfWork.Business;

public class ShippingAddressBusiness : BusinessBase<ShippingAddressRepository, ShippingAddress>
{
    public ShippingAddressBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}