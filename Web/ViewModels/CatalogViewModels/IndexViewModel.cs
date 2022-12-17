using ApplicationCore.DTOs;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        // Null values allowed because no filters may be applied
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        public int? BrandSelected { get; set; }
        public int? ProductTypeSelected { get; set; }
        public int? TotalProductCount { get; set; }
        public int? Page { get; set; }
    }
}
