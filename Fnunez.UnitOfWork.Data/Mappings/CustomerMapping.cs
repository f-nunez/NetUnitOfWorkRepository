using Fnunez.UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.UnitOfWork.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    private const int EmailMaxLength = 200;
    private const int FirstNameMaxLength = 100;
    private const int LastNameMaxLength = 100;

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Email)
            .HasMaxLength(EmailMaxLength);

        builder.Property(x => x.FirstName)
            .HasMaxLength(FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .HasMaxLength(LastNameMaxLength);

        builder.HasMany(x => x.ShippingAddresses)
            .WithOne()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}