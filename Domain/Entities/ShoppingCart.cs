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
            DateOfCreation = DateTime.Now;
        }
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime DateOfCreation { get; set; }

        // Add AddCartItem and CartItemQuantity methods here and flesh out CartItems and make them separate from catalogitems
    }
}

