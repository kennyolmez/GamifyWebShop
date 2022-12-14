using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(b => b.BuyerId)
                .IsRequired()
            .HasMaxLength(256);


            builder.HasMany(b => b.CartProducts)
                .WithOne(x => x.ShoppingCart)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
