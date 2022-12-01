using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class BrandDto
    {
        // Constructor for simplified conversion from domain entity to dto (for separation of concerns)
        public BrandDto(Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductDto> Products { get; set; } 
    }
}
