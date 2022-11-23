namespace Domain.Entities
{
    public class Product
    {
        // Constructor to reduce amount of code needed when instantiating the object (for example during seeding)
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
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ProductTypeId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public ProductType ProductType { get; set; }
    }

}