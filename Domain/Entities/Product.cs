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
        public Brand Brand { get; set; }
        public Category Category { get; set; }

        public readonly record struct ItemDetails
        {
            public string Name { get; }
            public string Description { get; }
            public decimal Price { get; }
        }
    }

}