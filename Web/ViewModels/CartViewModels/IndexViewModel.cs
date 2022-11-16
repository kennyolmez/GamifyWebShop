using ApplicationCore.DTOs;

namespace Web.ViewModels.CartViewModels
{
    public class IndexViewModel
    {
        public string Id { get; set; }
        public ShoppingCartDto UserCart { get; set; }
    }
}
