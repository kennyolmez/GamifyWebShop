using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OrderServices
    {
        private readonly ApplicationDbContext _context;

        public OrderServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CreateOrder(string? buyerId,
                                      int? cartId,
                                      string email,
                                      string fullName,
                                      string phoneNumber,
                                      string zipCode,
                                      string streetAddress,
                                      string city,
                                      string? deliveryAddressName)
        {
            var cart = new ShoppingCart("");
            var deliveryAddress = new DeliveryAddress(streetAddress, zipCode, city, deliveryAddressName); 

            // Generate order number
            // Generate the total cost etc

            if (buyerId is not null && cartId is not null)
            {
                cart = await _context.ShoppingCarts.Include(x => x.CartProducts).Where(x => x.BuyerId == buyerId).FirstAsync();
                
                _context.Orders.Add(new Order(buyerId)
                {
                    Products = cart.CartProducts,
                    DeliveryAddress = deliveryAddress,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    OrderNumber = Guid.NewGuid().ToString(),
                });

                _context.ShoppingCarts.Remove(cart);

                _context.SaveChanges();
            }


            // Fetch basket
            // Create order entity 
            // Save changes
            // Delete basket
        }
    }
}
