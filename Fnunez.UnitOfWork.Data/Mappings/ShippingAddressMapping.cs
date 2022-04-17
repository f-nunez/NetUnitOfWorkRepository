using Fnunez.UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.UnitOfWork.Data.Mappings;

public class ShippingAddressMapping : IEntityTypeConfiguration<ShippingAddress>
{
    private const int AddressMaxLEngth = 1000;
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Address)
            .HasMaxLength(AddressMaxLEngth);
    }
}