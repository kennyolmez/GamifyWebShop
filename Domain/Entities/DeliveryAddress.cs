using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string AddressName { get; set; }
        public string PostalCode { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
