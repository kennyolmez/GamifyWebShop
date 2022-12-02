using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ProductDto
    {
        //Temporary, will re-factor depending on needs
        // Constructor for simplified conversion from domain entity to dto (for separation of concerns)
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name; 
            Description = product.Description;
            PictureUrl = product.PictureUrl;
            Price = product.Price;
            Stock = product.Stock;
            BrandId = product.BrandId;
            Brand = new BrandDto(product.Brand);
            ProductTypeId = product.ProductTypeId;
            ProductType = new ProductTypeDto(product.ProductType);
            Reviews = product.Reviews?.Select(x => new ProductReviewDto(x)).ToList();
            Rating = product.Rating;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public double? Rating { get; set; }
        public BrandDto Brand { get; set; }
        public int ProductTypeId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public ProductTypeDto ProductType { get; set; }
        public ICollection<ProductReviewDto>? Reviews { get; set; }
    }
}
