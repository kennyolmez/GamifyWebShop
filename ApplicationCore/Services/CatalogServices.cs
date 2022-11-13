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


        public async Task<IEnumerable<ProductDto>> GetProducts(int? productTypeSelected, int? brandSelected, int? page, int pageSize)
        {
            List<ProductDto> outputQuery = new List<ProductDto>();

            // Because I want to render all the products on the home page if no filters are applied.

            if (productTypeSelected.HasValue && productTypeSelected > 0) // Bad solution because we shouldn't be getting 0.
            {
                var productTypeFilterQuery = await _context.Products
               .Include(p => p.Brand)
               .Include(p => p.ProductType)
               .ThenInclude(p => p.Category)
               .Where(x => x.ProductTypeId == productTypeSelected)
               .Paginate(page ?? 0, pageSize)
               .ToListAsync();

                outputQuery = productTypeFilterQuery.Select(x => new ProductDto(x)).ToList();
            }
            else if (brandSelected.HasValue && brandSelected > 0) // Bad solution because we shouldn't be getting 0.
            {
                var brandFilterQuery = await _context.Products // DbSet<Product>
                .Include(p => p.Brand) // IQueryable
                .Include(p => p.ProductType) // IQueryable
                .ThenInclude(p => p.Category) // IQueryable
                .Where(x => x.BrandId == brandSelected) // IQueryable
                .Paginate(page ?? 0, pageSize) // IQueryable
                .ToListAsync(); // async Task<List<T>>

                // Entity > DTO conversion
                outputQuery = brandFilterQuery.Select(x => new ProductDto(x)).ToList();
            }


            else
            {
                outputQuery = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.Category)
                .Select(x => new ProductDto(x))
                .Paginate(page ?? 0, pageSize)
                .ToListAsync();
            }

            return outputQuery;
        }

        public async Task<ProductDto?> GetProductById(int? id)
        {
            return await _context.Products.Include(p => p.Brand)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.Category)
                .Where(x => x.Id == id)
                .Select(x => new ProductDto(x))
                .FirstOrDefaultAsync();
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
        // Refactor?
        // To get product count without pagination for display purposes
        public async Task<int> GetProductCount(int? productTypeSelected, int? brandSelected)
        {
            if(productTypeSelected.HasValue)
            {
                return await _context.Products.Where(x => x.ProductTypeId == productTypeSelected).CountAsync();
            }
            else if(brandSelected.HasValue)
            {
                return await _context.Products.Where(x => x.BrandId == productTypeSelected).CountAsync();
            }
            else
            {
                return await _context.Products.CountAsync();
            }
        }
    }
}
 