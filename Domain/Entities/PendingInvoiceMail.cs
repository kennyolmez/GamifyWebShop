using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PendingInvoiceMail
    {
        public PendingInvoiceMail(string recipient, string orderNumber)
        {
            Recipient = recipient;
            OrderNumber = orderNumber;
        }

        public int Id { get; set; }
        public string Recipient { get; set; }

        public string OrderNumber { get; set; }

        public ICollection<ShoppingCartItem> InvoiceItems { get; set; }

        public decimal TotalOrderPrice()
        {
            decimal total = 0;

            foreach (var item in InvoiceItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
}
