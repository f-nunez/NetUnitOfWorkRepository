using Fnunez.UnitOfWork.Business.Abstractions;
using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Data.Repositories;
using Fnunez.UnitOfWork.Models;

namespace Fnunez.UnitOfWork.Business;

public class CustomerBusiness : BusinessBase<CustomerRepository, Customer>
{
    public CustomerBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}