using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class CategoryDto
    {
        // Constructor for simplified conversion from domain entity to dto (for separation of concerns)
        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ProductTypes = category.ProductTypes.Select(x => new ProductTypeDto(x)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductTypeDto> ProductTypes { get; set; }
    }
}
