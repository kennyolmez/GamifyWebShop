namespace Domain.Entities
{
    public class Category
    {
        // Constructor to reduce amount of code needed when instantiating the object (for example during seeding)
        public Category(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductType> Types { get; set; }
    }
}