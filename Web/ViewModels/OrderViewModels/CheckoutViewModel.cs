using ApplicationCore.DTOs;

namespace Web.ViewModels.OrderViewModels
{
    public class CheckoutViewModel
    {
        public string? OrderNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? StreetAddress { get; set; }
        public string? AddressName { get; set; } // Temporary
        public string? City { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ShoppingCartDto? UserCart { get; set; }
        public ICollection<ShoppingCartItemDto>? CartItems { get; set; }

        public decimal TotalPrice()
        {
            return Math.Round(UserCart.CartItems.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
