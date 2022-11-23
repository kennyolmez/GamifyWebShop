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
            CartItems = cart.CartProducts?.Select(x => new ShoppingCartItemDto(x)).ToList(); // has to be nullable
        }

        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<ShoppingCartItemDto> CartItems { get; set; } = new List<ShoppingCartItemDto>();
        public DateTime DateOfCreation { get; set; }
    }
}
