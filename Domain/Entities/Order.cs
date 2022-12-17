using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public Order(string buyerId)
        {
            BuyerId = buyerId;
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string BuyerId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public List<OrderItem> OrderItems { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }

        public decimal TotalOrderPrice()
        {
            var total = 0m;

            foreach (var item in OrderItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
}
