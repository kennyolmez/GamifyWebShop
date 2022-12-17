using ApplicationCore.DTOs;
using Humanizer.Bytes;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.CatalogViewModels
{
    public class _ProductViewModel
    {
        // Null values allowed because no filters may be applied
        public ProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public string? Comment { get; set;}
        public int Rating { get; set; } 
    }
}
