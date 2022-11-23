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
        public List<ShoppingCartItem> CartProducts { get; set; }
        public DateTime DateOfCreation { get; set; }

        public void AddItem(Product product, int quantity = 1)
        {
            if (!CartProducts.Any(i => i.Id == product.Id))
            {
                CartProducts.Add(new ShoppingCartItem(product.Name, product.Brand.Name, product.Price, quantity));
                return;
            }
            var duplicateProducts = CartProducts.First(i => i.Id == product.Id);
            duplicateProducts.AddQuantity(quantity);
        }

        public void RemoveEmptyItems()
        {
            CartProducts.RemoveAll(i => i.Quantity == 0);
        }

        public void SetNewBuyerId(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}

