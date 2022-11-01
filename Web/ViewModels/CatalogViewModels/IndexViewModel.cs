using ApplicationCore.DTOs;

namespace Web.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<BrandDto> Brand { get; set; }
        public IEnumerable<CategoryDto> Category { get; set; }
        public IEnumerable<ProductTypeDto> ProductType { get; set; }
        
        public bool FilterApplied { get; set; }
        // Null values allowed because no filters may be applied
        public int? BrandSelected { get; set; }
        public int? CategorySelected { get; set; }
        public int? ProductTypeSelected { get; set; }

    }
}
