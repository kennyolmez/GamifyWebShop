using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(int productId, string name, string brand, decimal price, string pictureUrl, int quantity)
        {
            ProductName = name;
            ProductBrand = brand;
            Price = price;
            Quantity = quantity;
            ProductId = productId;
            PictureUrl = pictureUrl;
        }

        public ShoppingCartItem() { }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public int ProductId { get; set; }
        // Nullable to support the severing of the relationship between parent and child
        public ShoppingCart ShoppingCart { get; set; } 

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
