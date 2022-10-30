using ApplicationCore.DTOs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CatalogServices
    {
        private readonly ApplicationDbContext _context;
        public CatalogServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.Brand)
                .ToListAsync();

            return products.Select(x => new ProductDto(x)).ToList();
        }

    }
}
