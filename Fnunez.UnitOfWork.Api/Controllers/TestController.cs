using Fnunez.UnitOfWork.Business;
using Fnunez.UnitOfWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.UnitOfWork.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private CustomerBusiness customerBusiness;
    private ShippingAddressBusiness shippingAddressBusiness;

    public TestController(CustomerBusiness customerBusiness, ShippingAddressBusiness shippingAddressBusiness)
    {
        this.customerBusiness = customerBusiness;
        this.shippingAddressBusiness = shippingAddressBusiness;
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(int numberOfCustomersToGenerate = 10)
    {
        List<Customer> customersToGenerate = await customerBusiness.Create(numberOfCustomersToGenerate);
        await shippingAddressBusiness.CreateByCustomers(customersToGenerate);
        return Ok();
    }

    [HttpGet("Read")]
    public async Task<IActionResult> Read()
    {
        var customers = await customerBusiness.GetAllWithTheirShippingAddress();
        return Ok(customers);
    }

    [HttpGet("Update")]
    public async Task<IActionResult> Update()
    {
        await customerBusiness.UpdateAll();
        await shippingAddressBusiness.UpdateAll();
        return Ok();
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete()
    {
        await customerBusiness.DeleteAll();
        await shippingAddressBusiness.DeleteAll();
        return Ok();
    }
}