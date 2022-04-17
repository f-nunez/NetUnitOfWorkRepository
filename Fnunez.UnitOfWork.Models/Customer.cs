using Fnunez.UnitOfWork.Models.Abstractions;

namespace Fnunez.UnitOfWork.Models;

public class Customer : ModelBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
}