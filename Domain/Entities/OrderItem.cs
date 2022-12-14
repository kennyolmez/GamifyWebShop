using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public PendingInvoiceMail? InvoiceMail { get; set; }
    }
}
