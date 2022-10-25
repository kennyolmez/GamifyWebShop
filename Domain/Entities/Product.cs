namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string EAN { get; set; }
        public int BrandId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public Brand Brand { get; set; }
        public int CategoryId { get; set; } // Foreign key for clarity, better being more explicit than not 
        public Category Category { get; set; }
    }

}