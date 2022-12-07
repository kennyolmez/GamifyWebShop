using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.CoravelMailing.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public List<ShoppingCartItemDto> Products { get; set; } 

        public decimal TotalOrderPrice()
        {
            decimal total = 0;

            foreach (var item in Products)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
}
