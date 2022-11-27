using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Web.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public string OrderNumber { get; set; }
        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }
        public string AddressName { get; set; } // Temporary
        public string County { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ShoppingCartDto UserCart { get; set; }

        public decimal TotalPrice()
        {
            return Math.Round(UserCart.CartItems.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
