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


        public async Task<IEnumerable<ProductDto>> GetAllProducts(int? productTypeSelected)
        {
            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .Where(x => x.ProductTypeId == productTypeSelected)
                .ToListAsync();

            var output = products.Select(x => new ProductDto(x)).ToList();

            return output;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypes()
        {
            var productTypes = await _context.ProductTypes
                .ToListAsync();

            var output = productTypes.Select(x => new ProductTypeDto(x)).ToList();

            return output;
        }

    }
}
