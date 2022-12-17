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
                                      string postalCode,
                                      string streetAddress,
                                      string city,
                                      string deliveryAddressName)
        {
            var cart = new ShoppingCart("");

            if (buyerId is not null && cartId is not null)
            {
                cart = await _context.ShoppingCarts.Include(x => x.CartProducts).Where(x => x.BuyerId == buyerId).FirstAsync();


                var order = new Order(buyerId)
                {
                    OrderItems = cart.CartProducts.Select(x => new OrderItem
                    {
                        ProductName = x.ProductName,
                        PictureUrl = x.PictureUrl,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        ProductBrand = x.ProductBrand,
                        ProductId = x.ProductId,
                    }).ToList(),
                    DeliveryAddress = new DeliveryAddress
                    {
                        StreetAddress = streetAddress,
                        City = city,
                        PostalCode = postalCode,
                        AddressName = deliveryAddressName,
                    },
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    OrderNumber = Guid.NewGuid().ToString(), // Temporary
                };

                _context.Orders.Add(order);
                _context.ShoppingCarts.Remove(cart);
                _context.SaveChanges();

                // If mail fails to send it will be added to the database
                _emailServices.TrySendInvoice(email, order.OrderNumber, order.OrderItems);
            }
        }
    }
}
