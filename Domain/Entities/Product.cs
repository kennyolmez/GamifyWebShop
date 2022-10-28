namespace Domain.Entities
{
    public class Product
    {
        // Constructor to reduce amount of code needed when instantiating the object (for example during seeding)
        public Product(string name, string description, string pictureUri, decimal price, int brandId, int categoryId, int stock)
        {
            Name = name;
            Description = description;
            PictureUri = pictureUri;
            Price = price;
            BrandId = brandId;
            CategoryId = categoryId;
            Stock = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public Category Category { get; set; }
    }

}