using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.StreetAddress)
                .IsRequired();
            builder.Property(a => a.AddressName)
                .IsRequired();
            builder.Property(a => a.PostalCode)
                .IsRequired();
            builder.Property(a => a.City)
                .IsRequired();

            builder.HasOne(a => a.Order)
                .WithOne(a => a.DeliveryAddress);

        }
    }
}
