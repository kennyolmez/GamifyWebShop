using ApplicationCore.DTOs;

namespace Web.ViewModels.CartViewModels
{
    public class IndexViewModel
    {
        // We actually want properties here instread of DTOs
        public ShoppingCartDto UserCart { get; set; }
        public decimal TotalPrice()
        {
            return Math.Round(UserCart.CartItems.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
