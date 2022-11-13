using ApplicationCore.Services;

namespace Web.Controllers
{
    public class CartController
    {
        private readonly CartServices _cartServices;
        public CartController(CartServices cartServices)
        {
            _cartServices = cartServices; 
        }
    }
}
