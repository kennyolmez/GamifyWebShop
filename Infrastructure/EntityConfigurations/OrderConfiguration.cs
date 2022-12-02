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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(b => b.DeliveryAddress).WithOne();

            builder.HasMany(b => b.Products).WithOne();

            builder.Property(b => b.BuyerId)
            .IsRequired()
            .HasMaxLength(256);

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
            .HasMaxLength(100);

            builder.Property(b => b.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Products)
                .IsRequired();     
        }
    }
}
