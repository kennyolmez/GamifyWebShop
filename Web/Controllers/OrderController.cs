using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Web.ViewModels.OrderViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderServices _orderServices;
        private readonly CartServices _cartServices;
        private string? userId = null;
        public OrderController(OrderServices orderServices, CartServices cartServices, ILogger<OrderController> logger)
        {
            _orderServices = orderServices;
            _cartServices = cartServices;
            _logger = logger;
        }

        public async Task<IActionResult> CreateOrder()
        {
            userId = User.Identity!.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["guest"];

            CheckoutViewModel vm = new CheckoutViewModel
            {
                UserCart = await _cartServices.GetOrCreateCart(userId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string userCartId)
        {
            userId = User.Identity!.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["guest"];

            await _orderServices.CreateOrder(userId, userCartId);

            return View();
        }
    }
}
