using Fnunez.UnitOfWork.Models.Abstractions;

namespace Fnunez.UnitOfWork.Models;

public class ShippingAddress : ModelBase
{
    public int CustomerId { get; set; }
    public string Address { get; set; }
    public bool IsMain { get; set; }
}