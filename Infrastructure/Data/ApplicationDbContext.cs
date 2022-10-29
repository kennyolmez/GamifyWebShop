using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    // Create application user
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // base accesses members of the base class (identitydbcontext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(Brand).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(Category).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(Product).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductType).Assembly);
        }
    }
}
