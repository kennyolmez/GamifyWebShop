using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(string buyerId)
        {
            BuyerId = buyerId;
        }
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public DateTime DateOfCreation { get; set; }
    }
}
