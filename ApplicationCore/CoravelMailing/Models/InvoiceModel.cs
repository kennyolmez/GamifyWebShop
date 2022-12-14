using ApplicationCore.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.CoravelMailing.Models
{
    public class InvoiceModel
    {
        public InvoiceModel(string recipient, List<OrderItem> orderItems, string orderNumber)
        {
            Recipient = recipient;
            OrderItems = orderItems;
            OrderNumber = orderNumber;
        }

        public InvoiceModel()
        {

        }
        public string OrderNumber { get; set; }
        public string Recipient { get; set; }
        public List<OrderItem> OrderItems { get; set; } 

        public decimal TotalOrderPrice()
        {
            decimal total = 0;

            foreach (var item in OrderItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        // Allows for mapping from PendingInvoiceMail to InvoiceModel
        public static InvoiceModel FromEntity(PendingInvoiceMail mail)
        {
            return new InvoiceModel
            {
                Recipient = mail.Recipient,
                OrderItems = mail.OrderItems.ToList()
            };
        }
    }
}
