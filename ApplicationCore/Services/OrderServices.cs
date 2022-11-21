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


        public async Task CreateOrder()
        {
            // Fetch basket
            // Create order entity 
            // Save changes
            // Delete basket
        }
    }
}
