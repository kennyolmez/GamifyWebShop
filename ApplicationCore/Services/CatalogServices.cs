using ApplicationCore.DTOs;
using ApplicationCore.Extensions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


        public async Task<IEnumerable<ProductDto>> GetProducts(int? productTypeSelected, int? brandSelected, string? productSearchString, int? page, int pageSize)
        {
            List<ProductDto> outputQuery = new List<ProductDto>();

            if (productSearchString is null)
            {
                var filteredProducts = await GetProductsByFilter(productTypeSelected, brandSelected, page, pageSize);

                outputQuery = filteredProducts.Select(x => new ProductDto(x)).ToList();
            }
            else
            {
                var filteredProducts = await GetProductsBySearchQuery(productSearchString, page, pageSize);

                outputQuery = filteredProducts.Select(x => new ProductDto(x)).ToList();
            }

            return outputQuery;
        }

        private async Task<IEnumerable<Product>> GetProductsByFilter(int? productTypeSelected, int? brandSelected, int? page, int pageSize)
        {
            List<Product> outputQuery = new List<Product>();

            if (productTypeSelected.HasValue && productTypeSelected > 0) // Bad solution because we shouldn't be getting 0.
            {
                var productTypeFilterQuery = await _context.Products
               .Include(p => p.Brand)
               .Include(p => p.ProductType)
               .ThenInclude(p => p.Category)
               .Where(x => x.ProductTypeId == productTypeSelected)
               .Paginate(page ?? 0, pageSize)
               .ToListAsync();

                outputQuery = productTypeFilterQuery.ToList();
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
                outputQuery = brandFilterQuery.ToList();
            }


            else
            {
                outputQuery = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.Category)
                .Paginate(page ?? 0, pageSize)
                .ToListAsync();
            }

            return outputQuery;
        }

        private async Task<IEnumerable<Product>> GetProductsBySearchQuery(string? productSearchString, int? page, int pageSize)
        {
            return await _context.Products.Where(p => p.Name.Contains(productSearchString!))
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .ThenInclude(p => p.Category)
                .Paginate(page ?? 0, pageSize)
                .ToListAsync();
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
        public async Task<int> GetProductCount(int? productTypeSelected, int? brandSelected, string? productSearchString)
        {
            if (productTypeSelected.HasValue)
            {
                return await _context.Products.Where(x => x.ProductTypeId == productTypeSelected).CountAsync();
            }
            else if (brandSelected.HasValue)
            {
                return await _context.Products.Where(x => x.BrandId == productTypeSelected).CountAsync();
            }
            else if (productSearchString is not null)
            {
                return await _context.Products.Where(p => p.Name.Contains(productSearchString!)).CountAsync();

            }
            else
            {
                return await _context.Products.CountAsync();
            }
        }
    }
}
