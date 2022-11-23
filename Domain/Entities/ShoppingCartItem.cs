using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(string name, string brand, decimal price, int quantity)
        {
            ProductName = name;
            ProductBrand = brand;
            Price = price;
            Quantity = quantity;
        }

        public ShoppingCartItem() { }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

 
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
