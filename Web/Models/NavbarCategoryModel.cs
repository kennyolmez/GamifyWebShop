using ApplicationCore.DTOs;

namespace Web.Models
{
    public class NavbarCategoryModel
    {
        public IEnumerable<BrandDto>? Brands { get; set; }
        public int? BrandId { get; set; }
        public IEnumerable<ProductTypeDto>? ProductTypes { get; set; }
        public int? ProductTypeId { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }

    }
}
