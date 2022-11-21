using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public Order(string buyerId, string email, string zipCode)
        {
            BuyerId = buyerId;
            Email = email;
            ZipCode = zipCode;
        }

        public int Id { get; set; }
        public string BuyerId { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;

        public ICollection<Product> Products { get; set; }

        //Unfinished for now, need to add units
        //public decimal Total()
        //{
        //    var total = 0m;
        //    foreach (var item in Products)
        //    {
        //        total += item.Price * item.Units;
        //    }
        //    return total;
        //}
    }
}
