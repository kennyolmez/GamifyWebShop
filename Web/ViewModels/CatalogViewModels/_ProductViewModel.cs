using ApplicationCore.DTOs;
using Humanizer.Bytes;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.CatalogViewModels
{
    public class _ProductViewModel
    {
        // Pluralize these 
        public IEnumerable<BrandDto>? Brand { get; set; }
        public IEnumerable<CategoryDto>? Category { get; set; }
        public IEnumerable<ProductTypeDto>? ProductType { get; set; }

        // Null values allowed because no filters may be applied
        public ProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public string? Comment { get; set;}
        public int Rating { get; set; } 
        public PaginationHelper? PaginationHelper { get; set; }
    }
}
