using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.EntityConfigurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasOne(pt => pt.Category)
                .WithMany(pt => pt.ProductTypes)
                .HasForeignKey(pt => pt.CategoryId);
        }
    }
}
