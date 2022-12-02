namespace Domain.Entities
{
    public class Product
    {
        public Product(string name, string description, string pictureUrl, decimal price, int brandId, int productTypeId, int stock)
        {
            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            Price = price;
            BrandId = brandId;
            ProductTypeId = productTypeId;
            Stock = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public double? Rating { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ProductTypeId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public ProductType ProductType { get; set; }
        public ICollection<ProductReview>? Reviews { get; set; }

        public void SetRating()
        {
            if (Reviews is not null && Reviews.Count() > 0)
            {
                Rating = Math.Round((double)Reviews.Sum(x => x.Rating) / Reviews.Count(), 1);
            }
            else
            {
                Rating = null;
            }
        }
    }
}