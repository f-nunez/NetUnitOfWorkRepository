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

    public async Task<List<Customer>> Create(int numberOfCustomersToGenerate)
    {
        List<Customer> newCustomers = new();
        for (int i = 0; i < numberOfCustomersToGenerate; i++)
        {
            var newCustomer = new Customer
            {
                Email = $"email.address.{i}@my_email.com",
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}"
            };
            newCustomers.Add(newCustomer);
        }

        await InsertRangeAsync(newCustomers);
        return newCustomers;
    }

    public async Task<List<Customer>> GetAllWithTheirShippingAddress()
    {
        var customers = await GetAsync(x => x.IsActive, includedProperties: new string[] { "ShippingAddresses" });
        return customers.ToList();
    }

    public async Task UpdateAll()
    {
        var customersToUpdate = await GetAsync(x => x.IsActive);
        if (customersToUpdate is null || !customersToUpdate.Any())
            return;

        for (int i = 0; i < customersToUpdate.Count(); i++)
        {
            customersToUpdate.ElementAt(i).Email = $"email.address.{i}@my_email.com";
            customersToUpdate.ElementAt(i).FirstName = $"FirstName{i}";
            customersToUpdate.ElementAt(i).LastName = $"LastName{i}";
        }

        await UpdateRangeAsync(customersToUpdate);
    }

    public async Task DeleteAll()
    {
        var customersToDelete = await GetAsync(x => x.IsActive);
        if (customersToDelete is null || !customersToDelete.Any())
            return;
            
        await DeleteRangeAsync(customersToDelete);
    }
}