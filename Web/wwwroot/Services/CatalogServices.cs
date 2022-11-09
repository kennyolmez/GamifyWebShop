using ApplicationCore.DTOs;
using ApplicationCore.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationCore.Services
{
    public class CatalogServices
    {
        private readonly ApplicationDbContext _context;
        public CatalogServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProductDto>> GetProducts(int? productTypeSelected, int? brandSelected, bool filterApplied, int? page, int pageSize)
        {
            List<ProductDto> filterOutput = new List<ProductDto>();

            // Because I want to render all the products on the home page if no filters are applied.
            if(filterApplied)
            {
                if(productTypeSelected.HasValue)
                {
                     var productTypeFilter = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.ProductType)
                    .ThenInclude(p => p.Category)
                    .Where(x => x.ProductTypeId == productTypeSelected)
                    .ToListAsync();

                    // Entity > DTO conversion
                    filterOutput = productTypeFilter.Select(x => new ProductDto(x)).ToList();
                }
                else if(brandSelected.HasValue)
                {
                    var brandFilter = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.ProductType)
                    .ThenInclude(p => p.Category)
                    .Where(x => x.BrandId == brandSelected)
                    .ToListAsync();

                    // Entity > DTO conversion
                    filterOutput = brandFilter.Select(x => new ProductDto(x)).ToList();
                }

                return filterOutput;
            }
            else
            {
                return await PaginationExtensions.Paginate(_context.Products.Include(p => p.Brand)
                    .Include(p => p.ProductType)
                    .ThenInclude(p => p.Category)
                    .Select(x => new ProductDto(x)), page ?? 0, pageSize)
                    .ToListAsync();
            }        
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypes()
        {
            var productTypes = await _context.ProductTypes
                .Include(pt => pt.Category)
                .ToListAsync();

            var output = productTypes.Select(pt => new ProductTypeDto(pt)).ToList();

            return output;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            var brands = await _context.Brands
                .ToListAsync();

            var output = brands.Select(x => new BrandDto(x)).ToList();

            return output;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var productTypes = await _context.Categories
                .ToListAsync();

            var output = productTypes.Select(x => new CategoryDto(x)).ToList();

            return output;
        }
    }
}
