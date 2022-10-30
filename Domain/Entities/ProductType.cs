using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductType
    {
        // Constructor to reduce amount of code needed when instantiating the object (for example during seeding)
        public ProductType(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
