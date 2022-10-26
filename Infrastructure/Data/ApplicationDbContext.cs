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
        DbSet<Brand> Brands { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

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
        }
    }
}
