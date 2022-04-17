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

    public async Task CreateByCustomers(List<Customer> customers)
    {
        List<ShippingAddress> shippingAddresses = new();
        foreach (var customer in customers)
        {
            var shippingAddress = new ShippingAddress
            {
                Address = $"{customer.FirstName}'s address...",
                CustomerId = customer.Id
            };
            shippingAddresses.Add(shippingAddress);
        }

        await InsertRangeAsync(shippingAddresses);
    }

    public async Task UpdateAll()
    {
        var shippingAddressesToUpdate = await GetAsync(x => x.IsActive);
        if (shippingAddressesToUpdate is null || !shippingAddressesToUpdate.Any())
            return;

        for (int i = 0; i < shippingAddressesToUpdate.Count(); i++)
            shippingAddressesToUpdate.ElementAt(i).Address = $"Address {i}";

        await UpdateRangeAsync(shippingAddressesToUpdate);
    }

    public async Task DeleteAll()
    {
        var ShippingAddressesToDelete = await GetAsync(x => x.IsActive);
        if (ShippingAddressesToDelete is null || !ShippingAddressesToDelete.Any())
            return;
        
        await DeleteRangeAsync(ShippingAddressesToDelete);
    }
}