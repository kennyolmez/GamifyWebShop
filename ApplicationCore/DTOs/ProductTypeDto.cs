using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ProductTypeDto
    {
        // Constructor for simplified conversion from domain entity to dto (for separation of concerns)
        public ProductTypeDto(ProductType productType)
        {
            Id = productType.Id;
            Name = productType.Name;
            CategoryId = productType.CategoryId;
            Category = new CategoryDto(productType.Category);
            Products = productType.Products.Select(x => new ProductDto(x)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
