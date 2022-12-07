using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ApplicationCore.Services
{
    public class OrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailServices _emailServices;

        public OrderServices(ApplicationDbContext context, EmailServices emailServices)
        {
            _context = context;
            _emailServices = emailServices;
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


            if (buyerId is not null && cartId is not null)
            {
                cart = await _context.ShoppingCarts.Include(x => x.CartProducts).Where(x => x.BuyerId == buyerId).FirstAsync();

                var order = new Order("buyerId")
                {
                    Products = cart.CartProducts,
                    DeliveryAddress = deliveryAddress,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    OrderNumber = Guid.NewGuid().ToString(), // Temporary
                };


                _context.Orders.Add(order);

                _emailServices.SendInvoice(email, order.OrderNumber, order.Products);

   
                _context.ShoppingCarts.Remove(cart);
                _context.SaveChanges();
            }
        }
    }
}
