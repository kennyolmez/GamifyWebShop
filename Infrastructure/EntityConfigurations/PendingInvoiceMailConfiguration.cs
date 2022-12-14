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
    public class PendingInvoiceMailConfiguration : IEntityTypeConfiguration<PendingInvoiceMail>
    {
        public void Configure(EntityTypeBuilder<PendingInvoiceMail> builder)
        {
            builder.Property(x => x.Recipient)
                .IsRequired();
        }
    }
}
