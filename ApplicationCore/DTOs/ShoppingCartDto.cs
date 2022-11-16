using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ShoppingCartDto
    {
        public ShoppingCartDto(ShoppingCart cart)
        {
            Id = cart.Id;
            BuyerId = cart.BuyerId;
            Products = cart.Products?.Select(x => new ProductDto(x)).ToList();
        }

        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public DateTime DateOfCreation { get; set; }
    }
}
