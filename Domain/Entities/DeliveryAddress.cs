using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DeliveryAddress
    {
        public DeliveryAddress(string streetAddress, string zipCode, string county, string city, string? name)
        {
            StreetAddress = streetAddress;
            ZipCode = zipCode;
            City = city;
            County = county;
            Name = name;
        }

        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string? Name { get; set; }
    }
}
