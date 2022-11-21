using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderServices _orderServices;
        public OrderController(OrderServices orderServices, ILogger<OrderController> logger)
        {
            _orderServices = orderServices;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            // Fetch basket/products
            // Use items to model viewmodel
            // Return to view
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            // Fetch basket
            // Execute CreateOrder method from OrderServices
            return View();
        }
    }
}
