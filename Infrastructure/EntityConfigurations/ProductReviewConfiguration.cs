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
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.Property(p => p.Comment)
                .HasMaxLength(1000)
                .IsRequired()
                .HasColumnName("Review");

            builder.Property(p => p.Rating)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(p => p.UserEmail)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(pt => pt.Product)
                .WithMany(pt => pt.Reviews)
                .HasForeignKey(pt => pt.ProductId); 
        }
    }
}
