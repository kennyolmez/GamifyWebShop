using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ShoppingCartItemDto
    {
        public ShoppingCartItemDto(ShoppingCartItem shoppingCartItem)
        {
            Id = shoppingCartItem.Id;
            Price = shoppingCartItem.Price;
            Quantity = shoppingCartItem.Quantity;
            ProductName = shoppingCartItem.ProductName;
            ProductBrand = shoppingCartItem.ProductBrand;
            PictureUrl = shoppingCartItem.PictureUrl;
        }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }

    }
}
