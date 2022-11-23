using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task CreateOrder(string? buyerId, string? cartId)
        {
            ShoppingCart cart = new ShoppingCart("");

            if (buyerId is not null && cartId is not null)
            {
            }
            // Fetch basket
            // Create order entity 
            // Save changes
            // Delete basket
        }
    }
}
