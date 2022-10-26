using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand
    {
        // Constructor to reduce amount of code needed when instantiating the object (for example during seeding)
        public Brand(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}
